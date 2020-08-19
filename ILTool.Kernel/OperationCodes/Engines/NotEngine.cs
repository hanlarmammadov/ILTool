using ILTool.Kernel.Exceptions;
using ILTool.Kernel.Metadata;
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
    /// Computes the bitwise complement of the integer value 
    /// on top of the stack and pushes the result onto the evaluation stack as the same type.
    /// </summary>
    public class NotEngine : PrimitivesEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("NotEngine: Evaluation stack should contain an element");

            ESSlot slot = context.EvalStack.Pop();
              
            if (!slot.TypeToken.IsPrimitive) 
                throw new OperandsNotSupportedByOperationException(ByteCode.Not, slot);
             
            var result = new ESSlot(ESSlotType.Val); 
            if (!IsPrimaryType(slot.TypeToken.PrimitiveType))
            {
                PrimitiveType primitiveType = GetCommonPrimaryType(slot.TypeToken.PrimitiveType, slot.TypeToken.PrimitiveType);
                //object converted = GetConvertOperation(slot.TypeToken.PrimitiveType, primitiveType)(slot.Val);
                result.Val = GetUnaryOperation(primitiveType, UnaryPrimitiveOpType.Not)(slot.Val); 
            }
            else
            {
                result.Val = GetUnaryOperation(slot.TypeToken.PrimitiveType, UnaryPrimitiveOpType.Not)(slot.Val);
            }
             
            result.TypeToken = slot.TypeToken; 

            context.EvalStack.Push(result); 
        }
    }
}
