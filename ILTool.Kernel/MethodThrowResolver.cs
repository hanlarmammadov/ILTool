using ILTool.Kernel.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class MethodThrowResolver : IExecutionInterruptionResolver
    {
        private ICallStack _callStack;

        public MethodThrowResolver(ICallStack callStack)
        {
            _callStack = callStack;
        }

        public bool Resolve(MethodStateMachine methodSM)
        {
            var exInfo = methodSM.State.ExeptionInfo;
            var currMethod = methodSM;
            EHTryEntity catchHandler = null;
            do
            {
                catchHandler = currMethod.ThrowLogicForMethod(exInfo);
                if (catchHandler != null)
                    break;

                //Continue to search an appropriate catch handler in the method one hierarchy upper
                currMethod = currMethod.Caller;
            }
            while (currMethod != null);
             
            methodSM.State.ExecutionInterruption = ExecutionInterruption.None;
            methodSM.State.ExeptionInfo = null;
            return true;
        }
    }
}
