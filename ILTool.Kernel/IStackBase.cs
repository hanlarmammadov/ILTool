using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public interface IStackBase<TItem>
    {
        void Push(TItem item); 
        TItem Pop(); 
        TItem Peek(); 
        Int32 Count { get; }
        void Clear();
    }
}
