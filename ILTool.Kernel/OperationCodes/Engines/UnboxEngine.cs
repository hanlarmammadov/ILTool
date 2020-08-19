using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Exceptions;
using ILTool.Kernel.Heap;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class UnboxEngine : PrimitivesEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain an element");

            ESSlot refSlot = context.EvalStack.Pop();

            if (refSlot.SType != ESSlotType.HORef || ((Int32)refSlot.Val) == GCHeapObj.NullObj.Addr)
                throw new InvalidOperationException("Stack contains an invalid data");

            //Get heap object by address 
            GCHeapObj hObj = context.GCHeap.GetObj((Int32)refSlot.Val);
         
            var typeObject = context.TypesHeap.GetTypeObject(hObj.TypeToken);

            if (typeObject==null)
                throw new InvalidOperationException("UnboxEngine: Type was not loaded");
            else if (!typeObject.TypeDesc.IsValueType)
                throw new InvalidOperationException("Reference type cannot be unboxed");

            ESSlot valSlot = new ESSlot()
            {
                SType = ESSlotType.Val,
                TypeToken = hObj.TypeToken
            };

            if (hObj.TypeToken.IsPrimitive)
                valSlot.Val = GetUnaryOperation(hObj.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStackRep)(hObj.Val);
            else
                valSlot.Val = hObj.Val;

            context.EvalStack.Push(valSlot);
        }
    }
}
