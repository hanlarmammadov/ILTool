using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class ExeptionInfo
    {
        public ExeptionInfo(ClassToken token, Int32 heapAddress)
        {
            Token = token;
            HeapAddress = heapAddress;
        }

        //public TypeDesc TypeDesc { get; private set; }
        public ClassToken Token { get; private set; }
        public Int32 HeapAddress { get; private set; }
    }
}
