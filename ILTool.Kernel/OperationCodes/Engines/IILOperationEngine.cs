using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public interface IILOperationEngine
    {
        void Execute(MethodContext context, MethodState state, object operand = null);
    }
}
