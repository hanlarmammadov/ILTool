﻿using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    //ToUInt32
    public class Conv_U4Engine : PrimitiveConversionEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain at least one element");

            ESSlot initSlot = context.EvalStack.Pop();

            if (!initSlot.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.Conv_U4, initSlot);

            ESSlot resultSlot = Convert(initSlot.Val, initSlot.TypeToken.PrimitiveType, PrimitiveType.UInt32);

            context.EvalStack.Push(resultSlot);
        }
    }
}
