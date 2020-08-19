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
    public class StLocEngine : PrimitivesEngine, IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Stack does not contain an element to be stored in locals");

            ESSlot slot = context.EvalStack.Pop();

            if (operand == null || !((operand is Int32) || (operand is Byte)))
                throw new OperandsNotSupportedByOperationException(ByteCode.Stloc, operand);

            Local local = context.Locals[(operand is Int32) ? (Int32)operand : (Int32)(Byte)operand];


            if (local.Description.TypeToken.IsPrimitive)
            {
                Object val = slot.Val;
                if (slot.TypeToken.PrimitiveType != local.Description.TypeToken.PrimitiveType)
                    val = GetConvertOperation(slot.TypeToken.PrimitiveType, local.Description.TypeToken.PrimitiveType)(val);

                if (!IsPrimaryType(local.Description.TypeToken.PrimitiveType))
                    val = GetUnaryOperation(local.Description.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStoreRep)(val);
                local.Val = val;
            }
            else
            {
                if (!context.TypeLoader.FirstIsOfTypeOrDerivedFromSecond(slot.TypeToken, local.Description.TypeToken))
                    throw new InvalidLocalsValueException("StLocEngine: Stack element cannot be store in local due to type mismatch");

                local.Val = slot.Val;
            }
        }
    }
}
