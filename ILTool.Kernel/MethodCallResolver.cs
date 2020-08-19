using System;
using System.Collections.Generic;
using System.Linq;

namespace ILTool.Kernel
{
    public class MethodCallResolver : IExecutionInterruptionResolver
    {
        private ICallStack _callStack;
        private IMethodStateMachineFactory _methodStateMachineFactory;

        public MethodCallResolver(ICallStack callStack, IMethodStateMachineFactory methodStateMachineFactory)
        {
            _callStack = callStack;
            _methodStateMachineFactory = methodStateMachineFactory;
        }

        public bool Resolve(MethodStateMachine methodSM)
        {
            //Create an execution model for calling method
            var cmExec = _methodStateMachineFactory.Create(methodSM.State.CallMethod);
            cmExec.Caller = methodSM;
            if (cmExec == null)
                throw new Exception("Method not found");

            //Fill calling method's arguments if any
            if (cmExec.MethodDesc.TakesArguments)
                cmExec.SetMethodArguments(methodSM.Context.EvalStack);

            //Add calling method to the call stack 
            _callStack.Push(cmExec);

            //Reset the flow
            methodSM.State.ExecutionInterruption = ExecutionInterruption.None;
            methodSM.State.CallMethod = null;

            return false;
        }
    }

}
