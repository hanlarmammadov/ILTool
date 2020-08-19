using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.ExceptionHandling
{
    public enum EHHandlerType
    {
        Catch,
        Finally
    }

    public class EHTryEntity
    {
        public Int32 Index { get; set; }
        public EHHandlerType HandlerType { get; set; }
        public Int32 TryOffset { get; set; }
        public Int32 TryLength { get; set; }

        public Int32 HandlerOffset { get; set; }
        public Int32 HandlerLength { get; set; }

        public ClassToken ExceptionToken { get; set; }

        public bool IncludesInstruction(Int32 instIndex)
        {
            return instIndex >= this.TryOffset && instIndex <= this.TryOffset + this.TryLength;
        }
        public bool IsLastInstruction(Int32 instIndex)
        {
            return instIndex == this.TryOffset + this.TryLength;
        }
    }
}
