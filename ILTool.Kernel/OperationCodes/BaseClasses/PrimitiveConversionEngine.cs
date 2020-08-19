using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.BaseClasses
{
    public class PrimitiveConversionEngine : PrimitivesEngine
    {
        protected ESSlot Convert(Object initialValue, PrimitiveType initialType, PrimitiveType targetType)
        {
            ESSlot resultSlot = new ESSlot(ESSlotType.Val);
            //Finding common type
            PrimitiveType commonType = initialType;
            if (!IsPrimaryType(initialType))
                commonType = PrimitiveType.Int32;
             
            resultSlot.Val = GetConvertOperation(commonType, targetType)(initialValue);
            resultSlot.TypeToken = GetTokenForPrimitive(targetType);
            return resultSlot;
        }

    }
}
