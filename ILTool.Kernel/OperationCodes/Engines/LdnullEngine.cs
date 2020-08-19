using ILTool.Kernel.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class LdnullEngine : IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            context.EvalStack.Push(new ESSlot()
            {
                SType = ESSlotType.HORef,
                TypeToken = null,
                Val = GCHeapObj.NullObj.Addr
            });
        }
    }
}
