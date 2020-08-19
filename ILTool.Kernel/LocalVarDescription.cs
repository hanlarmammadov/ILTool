using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class LocalVarDescription
    {
        public String Name;
        public ClassToken TypeToken;
        public ESSlotType SType;
        public bool IsPinned;
    }
}
