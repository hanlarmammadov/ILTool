using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using System;

namespace ILTool.Kernel.OperationCodes.Engines
{
    class CallvirtEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            //Pop "this" from stack and check to null
            ESSlot objThisSlot = context.EvalStack.Peek();
            Int32 thisRefAddr = (Int32)objThisSlot.Val;

            if (thisRefAddr == GCHeapObj.NullObj.Addr)
                throw new NullReferenceException("Object is null");

            //Get right method
            var methodDef = (operand as MethodToken);
            if (methodDef == null)
                throw new ArgumentException("Incorrect or null method definition");

            GCHeapObj thisObject = context.GCHeap.GetObj((Int32)objThisSlot.Val);


            //if (typeObjHeap == GCHeapObj.NullObj)
            //    throw new NullReferenceException("Type object is null");

            TypeObject typeObj = context.TypesHeap.GetTypeObject(thisObject.TypeToken);

            MethodDesc method = typeObj.VTable.GetMethod(methodDef);

            if (method == null)
                throw new InvalidOperationException("Method described by the provided metadata was not found in type's vtable");

            state.CallMethod = method;
            state.ExecutionInterruption = ExecutionInterruption.Call;
        }
    }
}
