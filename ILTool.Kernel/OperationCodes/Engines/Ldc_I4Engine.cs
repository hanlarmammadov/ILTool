using ILTool.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class Ldc_I4Engine : IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (operand == null)
                throw new ArgumentNullException("Ldc_I4Engine: operand is null");

            Int32 val;
            if (operand is Int32)
                val = (Int32)operand;
            else if (operand is UInt16)
                val = (Int32)(UInt16)operand;
            else if (operand is SByte)
                val = (Int32)(SByte)operand;
            else if (operand is Byte)
                val = (Int32)(Byte)operand;
            else
                throw new OperandsNotSupportedByOperationException(ByteCode.Ldc_I4, operand);

            var slot = new ESSlot()
            {
                TypeToken = TempTypeLocator.Int32Desc.Metadata,
                SType = ESSlotType.Val,
                Val = val
            };

            context.EvalStack.Push(slot);
        }
    }
}
