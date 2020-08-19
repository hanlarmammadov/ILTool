using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    class CallEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            MethodToken methodDef = (operand as MethodToken);
            if (methodDef == null)
                throw new ArgumentException("Incorrect or null method definition");
            MethodDesc method = null;
            if (methodDef.Owner != null)
            {
                TypeObject owner = context.TypesHeap.GetTypeObject(methodDef.Owner);
                method = owner.VTable.GetMethod(methodDef);
            }
            else
            {
                method = context.TypesHeap.GetGlobalMethod(methodDef);
            }
             
            if (method == null)
                throw new InvalidOperationException("Method described by the provided metadata was not found in type's vtable");

            state.CallMethod = method;
            state.ExecutionInterruption = ExecutionInterruption.Call;
        }
    }
}
