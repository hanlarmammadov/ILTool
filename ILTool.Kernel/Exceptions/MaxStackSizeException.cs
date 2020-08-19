using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Exceptions
{
    public class MaxStackSizeException : Exception
    {
        public MaxStackSizeException(String message)
            : base(message)
        {

        }
    }

}
