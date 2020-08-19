using ILTool.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class BrEngine : IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object operand = null)
        {
            state.JumpIndex = (Int32)operand;
        }
    }
}
