using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class SubEngine : BinaryArithmeticEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        { 
            if (context.EvalStack.Count < 2)
                throw new InvalidStackSizeException("SubEngine: Evaluation stack should contain two elements to be subtracted");

            ESSlot slot2 = context.EvalStack.Pop();
            ESSlot slot1 = context.EvalStack.Pop();
            
            if (!slot1.TypeToken.IsPrimitive || !slot2.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.Mul, slot1, slot2);

            ESSlot result = Compute(slot1, slot2, BinaryPrimitiveOpType.Sub);

            context.EvalStack.Push(result);
        }
    }
}
