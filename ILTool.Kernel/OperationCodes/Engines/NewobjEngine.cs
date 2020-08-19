using ILTool.Kernel.Domain;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class NewobjEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            //Check ctor token validity
            var ctorTkn = (operand as MethodToken);
            if (ctorTkn == null)
                throw new InvalidOperationException("Invalid constructor token");

            //Check if object type is loaded into domain
            if (!context.TypeLoader.TypeIsLoaded(ctorTkn.Owner))
                throw new InvalidOperationException("Type is not loaded into the domain");
            TypeObject typeObj = context.TypesHeap.GetTypeObject(ctorTkn.Owner);

            //Allocate an object in heap
            var objRef = context.GCHeap.AllocObjRetAddr(typeObj);

            //Put 'this' to the evaluation stack
            ESSlot thisSlot = new ESSlot()
            {
                TypeToken = typeObj.Token,
                SType = ESSlotType.HORef,
                Val = objRef
            };
            context.EvalStack.Push(thisSlot);

            //Initiate ctor call
            state.CallMethod = typeObj.VTable.GetMethod(ctorTkn);
            state.ExecutionInterruption = ExecutionInterruption.Call; 
        }
    }
}
