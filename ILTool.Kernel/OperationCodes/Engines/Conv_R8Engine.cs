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
    //ToDouble = 13,             //Conv_R8 = 0x6c,
    public class Conv_R8Engine : PrimitiveConversionEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain at least one element");

            ESSlot initSlot = context.EvalStack.Pop();

            if (!initSlot.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.Conv_R8, initSlot);

            ESSlot resultSlot = Convert(initSlot.Val, initSlot.TypeToken.PrimitiveType, PrimitiveType.Double);

            context.EvalStack.Push(resultSlot);
        }
    }
}
