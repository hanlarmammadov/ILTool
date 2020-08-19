using ILTool.Kernel.Descriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public interface IMethodStateMachineFactory
    {
        MethodStateMachine Create(MethodDesc methodDesc);
    }
}
