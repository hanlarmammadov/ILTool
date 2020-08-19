using ILTool.Kernel.Descriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class CompiledModel
    {
        public Dictionary<string, MethodDesc> Methods;
        public CompiledModel()
        {
            Methods = new Dictionary<string, MethodDesc>();
        }
        public CompiledModel AddMethod(MethodDesc method)
        {
            Methods.Add(method.Name, method);
            return this;
        }
    }
}
