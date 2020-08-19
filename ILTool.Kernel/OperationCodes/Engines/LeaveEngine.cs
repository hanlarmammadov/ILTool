using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class LeaveEngine : IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object operand = null)
        {
            state.FlowInterruption = FlowInterruption.LeaveBlock;
            state.LeaveRequest = (Int32)operand;
        }
    }
}
