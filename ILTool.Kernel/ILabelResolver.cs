using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public interface ILabelResolver
    {
        void ResolveInstructionLabels(ILInstruction[] instructions);
        Int32 GetNumber(string lname);
    }
}
