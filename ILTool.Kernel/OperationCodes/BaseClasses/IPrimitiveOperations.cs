using ILTool.Kernel.Metadata;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.BaseClasses
{
    public interface IPrimitiveOperations
    {
        UnaryOperation GetUnaryOperation(PrimitiveType primitive, UnaryPrimitiveOpType operation);
        BinaryOperation GetBinaryOperation(PrimitiveType primitive, BinaryPrimitiveOpType operation);
        UnaryOperation GetConvertOperation(PrimitiveType fromPrimitive, PrimitiveType toPrimitive);
        bool IsPrimaryType(PrimitiveType primitive);
        PrimitiveType GetCommonPrimaryType(PrimitiveType primitive1, PrimitiveType primitive2);
        ClassToken GetTokenForPrimitive(PrimitiveType primitive);
        //PrimitiveType GetPrimaryType(PrimitiveType primitive);
    }
}
