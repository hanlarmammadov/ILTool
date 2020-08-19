using ILTool.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class BrfalseEngine : BrEngine
    {
        public override void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain an element to be evaluated");

            var slot = context.EvalStack.Pop();

            if ((slot.Val.Equals(false)) || slot.Val.Equals(0) || (slot.SType == ESSlotType.HORef && slot.Val.Equals(Heap.GCHeapObj.NullObj)))
                base.Execute(context, state, operand);
        }
    }
}
