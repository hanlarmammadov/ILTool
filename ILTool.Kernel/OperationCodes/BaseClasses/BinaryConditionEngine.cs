using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.BaseClasses
{
    public abstract class BinaryConditionEngine : PrimitivesEngine
    {
        protected bool ConditionSatisfied(ESSlot slot1, ESSlot slot2, BinaryPrimitiveOpType binOperation)
        {
            Object val1Converted = null;
            Object val2Converted = null;
            PrimitiveType commonPrimType = default(PrimitiveType);

            if (slot1.TypeToken == slot2.TypeToken && IsPrimaryType(slot1.TypeToken.PrimitiveType))
            {
                val1Converted = slot1.Val;
                val2Converted = slot2.Val;
                commonPrimType = slot1.TypeToken.PrimitiveType;
            }
            else
            {
                //Get common primary type
                commonPrimType = GetCommonPrimaryType(slot1.TypeToken.PrimitiveType, slot2.TypeToken.PrimitiveType);
                //Convert slot vals
                val1Converted = slot1.Val;
                val2Converted = slot2.Val;
                //val1Converted = GetConvertOperation(slot1.TypeToken.PrimitiveType, commonPrimType)(slot1.Val);
                //val2Converted = GetConvertOperation(slot2.TypeToken.PrimitiveType, commonPrimType)(slot2.Val);
            }

            return (Boolean)GetBinaryOperation(commonPrimType, binOperation)(val1Converted, val2Converted);
        }
    }
}
