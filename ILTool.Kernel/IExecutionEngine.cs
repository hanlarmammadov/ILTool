using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public interface IExecutionEngine
    {
        void Start();
        Object EntryPointReturn { get; set; } 
    }
}
