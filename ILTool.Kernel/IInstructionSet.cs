using ILTool.Kernel.OperationCodes.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public interface IILOperationSet
    {
        IILOperationEngine GetEngine(ByteCode code);
    }
}
