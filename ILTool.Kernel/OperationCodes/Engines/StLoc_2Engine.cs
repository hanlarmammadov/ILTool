using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class StLoc_2Engine : StLocEngine
    {
        public override void Execute(MethodContext context, MethodState state, object operand = null)
        {
            base.Execute(context, state, 2);
        }
    }
}
