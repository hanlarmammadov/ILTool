﻿using ILTool.Kernel.Exceptions;
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
    class BeqEngine : BinaryConditionEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 2)
                throw new InvalidStackSizeException("BeqEngine: Evaluation stack should contain two elements to be added");

            ESSlot slot2 = context.EvalStack.Pop();
            ESSlot slot1 = context.EvalStack.Pop();
             
            if (!slot1.TypeToken.IsPrimitive || !slot2.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.And, slot1, slot2);

            if (ConditionSatisfied(slot1, slot2, BinaryPrimitiveOpType.Equal))
                state.JumpIndex = (Int32)operand;
        }
    }
}
