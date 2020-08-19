using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.ExceptionHandling;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using ILTool.Kernel.OperationCodes;
using ILTool.Kernel.OperationCodes.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class MethodStateMachine
    {
        public MethodDesc MethodDesc { get; set; }
        public MethodContext Context { get; set; }
        public MethodState State { get; set; }
        public MethodStateMachine Caller { get; set; }
        private IILOperationSet _operationSet;

        public static MethodStateMachine CreateForMethod(MethodDesc methodDescription, IGCHeap gcHeap, ITypesHeap typesHeap, ITypeLoader typeLoader, IILOperationSet operationSet)
        {
            MethodStateMachine execModel = new MethodStateMachine()
            {
                _operationSet = operationSet,
                MethodDesc = methodDescription,
                Context = ILTool.Kernel.MethodContext.CreateForMethod(methodDescription, gcHeap, typesHeap, typeLoader),
                State = new MethodState()
            };
            return execModel;
        }

        public void SetMethodArguments(IEvalStack callerEvalStack)
        {
            MtdArg[] args = Context.Args;
            for (int i = args.Length - 1; i >= 0; i--)
                args[i].Val = callerEvalStack.Pop().Val;
        }

        public void TakeMethodReturnValue(IEvalStack callieEvalStack)
        {
            ESSlot topSlot = callieEvalStack.Pop();
            Context.EvalStack.Push(topSlot);
        }

        private void ResolveFlowInterruption()
        {
            switch (State.FlowInterruption)
            {
                case FlowInterruption.LeaveBlock:
                    LeaveBlock();
                    break;
                case FlowInterruption.EndFinally:
                    Endfinally();
                    break;
            }
        }

        private bool TryMoveToNextInstruction(out ILInstruction nextInst)
        {
            nextInst = null;
            bool success = false;
            if (State.JumpIndex != -1)
            {
                State.CurrInstIdx = State.JumpIndex;
                State.JumpIndex = -1;
                success = true;
            }
            else if (State.CurrInstIdx < MethodDesc.Instructions.Length - 1)
            {
                State.CurrInstIdx += 1;
                success = true;
            }

            if (success)
                nextInst = MethodDesc.Instructions[State.CurrInstIdx];

            return success;
        }

        public void ExecInstructions()
        {
            int num = 0;
            try
            {
                while (State.ExecutionInterruption == ExecutionInterruption.None && State.FlowInterruption == FlowInterruption.Next)
                {
                    ILInstruction inst;
                    if (!TryMoveToNextInstruction(out inst))
                    {
                        State.ExecutionInterruption = ExecutionInterruption.Finished;
                        break;
                    }

                    IILOperationEngine insEn = _operationSet.GetEngine(inst.ILOperation);
                    insEn.Execute(Context, State, inst.Operand);

                    inst.ExecuteCallback();

                    if (State.FlowInterruption != FlowInterruption.Next)
                        ResolveFlowInterruption();
                    num++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Operation execution terminated due to an error. See inner exception", ex);
            }
        }

        private bool AddFinalliesToStack(Int32 upToIndex = 0)
        {
            Int32 count = 0;
            var finallies = this.MethodDesc.EHTable.GetFinallyHandlers(State.CurrInstIdx, upToIndex);
            if (finallies != null)
            {
                foreach (var fin in finallies)
                    State.BlockJump.Push(BlockInfo.Finally(fin.HandlerOffset));
                count = finallies.Count;
            }
            return count!=0;
        }

        private void JumpToBlock(BlockInfo blockInfo)
        {
            State.JumpIndex = blockInfo.JumpIndex;
            if (blockInfo.Type == BlockType.Catch)
            {
                Context.EvalStack.Push(new ESSlot()
                {
                    TypeToken = blockInfo.ExInfo.Token,
                    SType = ESSlotType.HORef,
                    Val = blockInfo.ExInfo.HeapAddress
                });
            }
        }

        public EHTryEntity ThrowLogicForMethod(ExeptionInfo exeptionInfo)
        {
            EHTryEntity catchHandler = this.MethodDesc.EHTable.GetCatchIfExists(State.CurrInstIdx, exeptionInfo.Token);
            Int32 catchIndex = 0;
            if (catchHandler != null)
            {
                State.BlockJump.Push(BlockInfo.Catch(catchHandler.HandlerOffset, exeptionInfo));
                catchIndex = catchHandler.Index;
            }
            else
                State.MethodPendingFinallies = true;

            AddFinalliesToStack(catchIndex);

            var blockInfo = State.BlockJump.Pop();
            JumpToBlock(blockInfo);

            return catchHandler;
        }

        public bool FinalliesBeforeReturn()
        {
            State.FlowInterruption = FlowInterruption.Next;

            if (AddFinalliesToStack())
                State.MethodPendingFinallies = true;

            return State.MethodPendingFinallies;
        }

        private void LeaveBlock()
        {
            State.BlockJump.Push(BlockInfo.Leave(State.LeaveRequest));


            EHTryEntity finallyBlock = this.MethodDesc.EHTable.GetFinallyForLeaveInst(State.CurrInstIdx);

            // if  finallyBlock exists, it means that leave instruction is inside of try part of try-finally block
            // if not, then it is either the last instruction of try or the catch part of try-catch block  
            if (finallyBlock != null)
            {
                //Do not touch leaving address, and jump control to finally block first
                State.BlockJump.Push(BlockInfo.Finally(finallyBlock.HandlerOffset));
            }

            State.FlowInterruption = FlowInterruption.Next;

            var blockInfo = State.BlockJump.Pop(); 
            JumpToBlock(blockInfo);
        }

        private void Endfinally()
        { 
            //Pop 'latest' leave address and jump to it
            if (State.BlockJump.Count != 0)
            {
                var blockInfo = State.BlockJump.Pop();
                JumpToBlock(blockInfo);
            }
            else
            {
                if (State.MethodPendingFinallies)
                    State.ExecutionInterruption = ExecutionInterruption.Finished;
            }
            State.FlowInterruption = FlowInterruption.Next;
        }
        
    }
}
