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
    public class BoxEngine : PrimitivesEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain an element to be boxed");

            ESSlot valueSlot = context.EvalStack.Pop();

            if (valueSlot.SType != ESSlotType.Val)
                throw new InvalidOperationException("Stack contains an invalid data that cannot be boxed");
            
            //Check if type is loaded
            if (!context.TypeLoader.TypeIsLoaded(valueSlot.TypeToken))
                throw new InvalidOperationException("Type is not loaded into the domain");

            if (valueSlot.TypeToken.IsPrimitive)
            { 
                if (!IsPrimaryType(valueSlot.TypeToken.PrimitiveType))
                    valueSlot.Val = GetUnaryOperation(valueSlot.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStoreRep)(valueSlot.Val);
            }

            TypeObject typeObj = context.TypesHeap.GetTypeObject(valueSlot.TypeToken);
            
            GCHeapObj hObj = context.GCHeap.AllocObj(typeObj);
            hObj.Val = valueSlot.Val;

            ESSlot refSlot = new ESSlot()
            {
                TypeToken = valueSlot.TypeToken,
                SType = ESSlotType.HORef,
                Val = hObj.Addr
            };
            context.EvalStack.Push(refSlot);
        }
    }
}
