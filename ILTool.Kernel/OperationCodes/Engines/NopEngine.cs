using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class NopEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            string obj = "empty";
            if (context.EvalStack.Count != 0)
                obj = context.EvalStack.Peek().Val.ToString();
            Debug.Print($"Debug: {operand}, stack top: {obj}");
        }
    }
}
