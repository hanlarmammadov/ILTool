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
    /// <summary>
    /// Compares two values. If they are equal, the integer value 1 (int32) 
    /// is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.
    /// </summary>
    public class CeqEngine : BinaryConditionEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 2)
                throw new InvalidStackSizeException("CeqEngine: Evaluation stack should contain two elements to be compared");

            ESSlot slot2 = context.EvalStack.Pop();
            ESSlot slot1 = context.EvalStack.Pop();

            ESSlot result = new ESSlot(ESSlotType.Val, TempTypeLocator.Int32Desc.Metadata);

            if (!slot1.TypeToken.IsPrimitive || !slot2.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.Ceq, slot1, slot2);

            result.Val = (Int32)(ConditionSatisfied(slot1, slot2, BinaryPrimitiveOpType.Equal) ? 1 : 0);
                 
            context.EvalStack.Push(result);
        }
    }
}

