using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Heap
{
    public class GCHeapObj
    {
        public Int32 Addr;
        public Object Val;
        public Int32 RCount;
        //public String Type;
        public bool IsGCollected;
        //public Int32 TypeObjAddr;
        public ClassToken TypeToken;

        static GCHeapObj()
        {
            NullObj = new GCHeapObj() { Addr = 0 };
        }

        public static readonly GCHeapObj NullObj;
    }
}
