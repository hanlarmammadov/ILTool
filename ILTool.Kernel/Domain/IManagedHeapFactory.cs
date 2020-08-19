using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Domain
{
    public interface IGCHeapFactory
    { 
        IGCHeap Create();
    }
}
