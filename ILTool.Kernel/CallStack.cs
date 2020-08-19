using ILTool.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{ 
   public class CallStack : BaseStack<MethodStateMachine>, ICallStack
    {
        public CallStack(Int32 maxSize = Int16.MaxValue)
            : base(maxSize)
        {

        }

        protected override MaxStackSizeException GetMaxStackSizeException()
        {
            throw new MaxStackSizeException("Evaluation stack max size has been reached");
        }
    }
}
