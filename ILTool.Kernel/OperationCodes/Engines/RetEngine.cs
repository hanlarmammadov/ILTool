using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class RetEngine : PrimitivesEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.ReturnsValue && context.EvalStack.Count != 1)
                throw new InvalidStackSizeException("To return value from method, stack should contain a single value");
            else if (!context.ReturnsValue && context.EvalStack.Count != 0)
                throw new InvalidStackSizeException("For methods that do not return value stack should be empty before returning");

            if (context.ReturnsValue)
            {
                if (context.EvalStack.Count != 1)
                    throw new InvalidStackSizeException("To return value from method, stack should contain a single value");
            }
            else
            {
                if (context.EvalStack.Count != 0)
                    throw new InvalidStackSizeException("For methods that do not return value stack should be empty before returning");
            }
            state.BlockJump.Clear();
            state.ExecutionInterruption = ExecutionInterruption.Return;
        }
    }
}
