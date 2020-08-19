using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System.Collections.Generic;

namespace ILTool.Kernel.Domain
{ 
    public interface ITypeLoader
    { 
        void LoadType(TypeDesc typeDesc);
        bool TypeIsLoaded(ClassToken typeMetadata); 
        void LoadGlobalMethods(List<MethodDesc> globalMethods);
        bool FirstIsOfTypeOrDerivedFromSecond(ClassToken checkingClass, ClassToken targetClass);
    }
}
