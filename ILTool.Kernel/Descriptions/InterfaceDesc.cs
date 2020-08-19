using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Descriptions
{
    public class InterfaceDesc: DescBase
    {
        public string Name { get; set; }
        public List<InterfaceDesc> InheritedInterfaces { get; set; }
    }
}
