using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class LdLoc_1Engine : LdLocEngine
    {
        public override void Execute(MethodContext context, MethodState state, object localInx = null)
        {
            base.Execute(context, state, 1);
        }
    }
}
