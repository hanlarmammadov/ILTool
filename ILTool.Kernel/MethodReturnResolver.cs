using ILTool.Kernel.Metadata;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;

namespace ILTool.Kernel
{
    public class MethodReturnResolver : IExecutionInterruptionResolver
    {
        private ICallStack _callStack;
        private IPrimitiveOperations _primitiveOperations;

        public delegate void SetEntryPointRet(Object retVal);
        private SetEntryPointRet _setEntryPointRet;

        protected void EntryPointRet(ESSlot slot, ClassToken returnType)
        {
            Object result = null;

            if ((returnType.IsPrimitive) && (!_primitiveOperations.IsPrimaryType(slot.TypeToken.PrimitiveType)))
                result = _primitiveOperations.GetUnaryOperation(slot.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStoreRep)(slot.Val);
            else
                result = slot.Val;

            _setEntryPointRet(result);
        }

        public MethodReturnResolver(ICallStack callStack, IPrimitiveOperations primitiveOperations, SetEntryPointRet setEntryPointRet)
        {
            _callStack = callStack;
            _setEntryPointRet = setEntryPointRet;
            _primitiveOperations = primitiveOperations;
        }

        public bool Resolve(MethodStateMachine methodSM)
        {
            //Pop away finished method
            _callStack.Pop();

            //Take its return value if needed
            if (methodSM.Context.ReturnsValue && _callStack.Count != 0)
                _callStack.Peek().TakeMethodReturnValue(methodSM.Context.EvalStack);

            if (methodSM.MethodDesc.IsEntryPoint && methodSM.MethodDesc.ReturnType != null)
                EntryPointRet(methodSM.Context.EvalStack.Peek(), methodSM.MethodDesc.ReturnType);

            //Find finally block. If exists, jump to its offset and continue execution. if not, break
            if (methodSM.FinalliesBeforeReturn())
            {
                methodSM.State.ExecutionInterruption = ExecutionInterruption.None;
                return true;
            }
            else
                return false;
        }
    }
}
