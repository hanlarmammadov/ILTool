using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Exceptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class ThrowEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Throw: Evaluation stack should contain at least one element");

            var slot = context.EvalStack.Pop();

            if (slot.SType != ESSlotType.HORef)
                throw new InvalidOperationException("Throw: Evaluation stack should contain an HORef object");

            if (slot.Val == null)
                throw new InvalidOperationException("Throw: Evaluation stack should contain a not null object");

            //Only for C#. Should be removed. 
            if (!context.TypeLoader.FirstIsOfTypeOrDerivedFromSecond(slot.TypeToken, TempTypeLocator.ExceptionDesc.Metadata))
                throw new InvalidOperationException("Throw: thowing object can be of type derived from System.Exception object");

            context.EvalStack.Clear();
            state.BlockJump.Clear();

            //Load object info from heap
            //var typObj = context.TypesHeap.GetTypeObject(slot.TypeToken);

            state.ExecutionInterruption = ExecutionInterruption.Throw;
            state.ExeptionInfo = new ExeptionInfo(slot.TypeToken, (Int32)slot.Val);
        }
    }
}
