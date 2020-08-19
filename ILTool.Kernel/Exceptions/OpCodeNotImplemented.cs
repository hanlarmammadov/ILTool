using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Exceptions
{
    public class OpCodeNotImplementedException : Exception
    {
        public ByteCode ByteCode { get; private set; }

        public OpCodeNotImplementedException(ByteCode byteCode, string message)
            : base(message)
        {
            ByteCode = byteCode;
        }
    }
}
