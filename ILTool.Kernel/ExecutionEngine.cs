using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain; 
using ILTool.Kernel.Heap;
using ILTool.Kernel.OperationCodes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace ILTool.Kernel
{
    public class ExecutionEngine : IExecutionEngine
    {
        private CompiledModel _compiledModel; 
        private ICallStack _callStack;
        private IMethodStateMachineFactory _methodStateMachineFactory;
        private Dictionary<ExecutionInterruption, IExecutionInterruptionResolver> _interruptionResolvers;

        public Object EntryPointReturn { get; set; }
         
        public ExecutionEngine(CompiledModel compiledModel, IGCHeap gcHeap, ITypesHeap typesHeap, ITypeLoader typeLoader, ICallStack callStack, IILOperationSet ilOperationSet)
        {
            _compiledModel = compiledModel; 
            _callStack = callStack;
            _methodStateMachineFactory = new MethodStateMachineFactory(gcHeap, typesHeap, typeLoader, ilOperationSet);
            FillInterruptionResolvers();
        }

        private void FillInterruptionResolvers()
        {
            _interruptionResolvers = new Dictionary<ExecutionInterruption, IExecutionInterruptionResolver>();
            _interruptionResolvers.Add(ExecutionInterruption.Call, new MethodCallResolver(_callStack, _methodStateMachineFactory));
            _interruptionResolvers.Add(ExecutionInterruption.Return, new MethodReturnResolver(_callStack, new PrimitivesEngine(), (r) => { this.EntryPointReturn = r; }));
            _interruptionResolvers.Add(ExecutionInterruption.Throw, new MethodThrowResolver(_callStack));
            _interruptionResolvers.Add(ExecutionInterruption.Finished, new MethodFinishedResolver(_callStack));
        }
        
        public void Start()
        {
            try
            {
                MethodDesc entryPoint = _compiledModel.Methods.Values.Where(d => d.IsEntryPoint).SingleOrDefault();
                if (entryPoint == null)
                    throw new InvalidOperationException("Entry point not found");
                MethodStateMachine entryPointExecModel = _methodStateMachineFactory.Create(entryPoint);
                _callStack.Push(entryPointExecModel);
                ProcessCallStack();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ProcessCallStack()
        {
            while (_callStack.Count != 0)
            {
                MethodStateMachine methodSM = _callStack.Peek();
                while (true)
                {
                    methodSM.ExecInstructions();

                    if (_interruptionResolvers[methodSM.State.ExecutionInterruption].Resolve(methodSM))
                        continue;
                    else
                        break;
                }
            }
        }
    }
}
