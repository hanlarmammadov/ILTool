using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Exceptions
{
    public class OperandsNotSupportedByOperationException : Exception
    {
        public new string Message { get; private set; }

        public OperandsNotSupportedByOperationException(ByteCode byteCode, params object[] operands)
        {
            StringBuilder sb = new StringBuilder()
                .Append($"Operation [{Enum.GetName(typeof(ByteCode), byteCode)}] does not support the given set of arguments:");
            if (operands != null)
                for (int i = 0; i < operands.Length; i++)
                    sb.Append($" [{operands[i].GetType().Name}]");
            this.Message = sb.ToString();
        }
    }
}
