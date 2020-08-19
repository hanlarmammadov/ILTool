using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Heap
{
    public interface IGCHeap
    {
        GCHeapObj AllocObj(TypeObject typeObj);
        Int32 AllocObjRetAddr(TypeObject typeObject);
        //GCHeapObj AllocObjType();
        GCHeapObj GetObj(Int32 addr);
        //HeapObj GetObjType(Int32 addr);
        //GCHeapObj GetTypeObjForObj(Int32 objAddr);
    }
}
