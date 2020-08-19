using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Heap
{
    public interface ITypesHeap
    {
        TypeObject GetTypeObject(ClassToken token);
        void AddTypeObject(ClassToken token, TypeObject typeObject);
        MethodDesc GetGlobalMethod(MethodToken token);
        void AddGlobalMethod(MethodToken token, MethodDesc methodDesc);
        bool ContainsType(ClassToken token);
    } 
}
