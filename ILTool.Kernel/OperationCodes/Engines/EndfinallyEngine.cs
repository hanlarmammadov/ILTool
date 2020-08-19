using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class EndfinallyEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            //if (state.BlockJump.Count()!=0)
            //    state.FlowInterruption = FlowInterruption.ContinueAfterFinally;
            //else
            //    state.FlowInterruption = FlowInterruption.NextFinally;

            state.FlowInterruption = FlowInterruption.EndFinally;

            //state.OpertionRequestsBlockLeave = false;

            //if (state.FinallyContinuation == FinallyContinuation.FinallyBeforeRet)
            //    state.FlowInterruption = FlowInterruption.NextFinally;
            //else if (state.FinallyContinuation == FinallyContinuation.FinallyAfterLeave)

            //else
            //    throw new InvalidOperationException("Invalid finally continuation");

            //Reset FinallyContinuation to default
            //state.FinallyContinuation = FinallyContinuation.No;
        }
    }
}
