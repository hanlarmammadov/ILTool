using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILTool.Kernel.Exceptions;

namespace ILTool.Kernel
{
    public class EvalStack : BaseStack<ESSlot>, IEvalStack
    {
        public EvalStack(Int32 maxSize)
            : base(maxSize)
        {

        }

        protected override MaxStackSizeException GetMaxStackSizeException()
        {
            throw new MaxStackSizeException("Evaluation stack max size has been reached");
        }
    }
}
