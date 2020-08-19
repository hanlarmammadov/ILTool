using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class MethodArgDescription
    {
        public String Name;
        //public TypeDesc Type;
        public ESSlotType SType;

        public virtual int Index { get; set; }
        public bool IsIn { get; set; }
        public bool IsRetval { get; set; }
        public bool IsLcid { get; set; }
        public bool IsOptional { get; set; }
        public bool IsOut { get; set; }
        public bool HasDefaultValue { get; set; }
        public Object DefaultValue { get; set; }
        public int MetadataToken { get; set; }
        public ClassToken TypeToken { get; set; }  
    }
}
