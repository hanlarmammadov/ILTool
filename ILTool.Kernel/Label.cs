using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class Label
    {
        public string Name { get; private set; }
        public Label(string name)
        {
            Name = name.Trim().ToLower();
        }
    }
}
