using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class Brtrue_sEngine : BrEngine
    {
        public override void Execute(MethodContext context, MethodState state, object operand = null)
        {
            base.Execute(context, state, operand);
        }
    }
}
