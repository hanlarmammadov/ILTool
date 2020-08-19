using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Domain
{ 
    public class TypeLoader : ITypeLoader
    {
        private ITypesHeap _typesHeap;

        private void LoadTypeAndAncestorsRecursively(TypeDesc typeDesc)
        {
            if (typeDesc == null || _typesHeap.ContainsType(typeDesc.Metadata))
                return;

            LoadTypeAndAncestorsRecursively(typeDesc.BaseClass);
            CreateTypeObject(typeDesc);
        }

        private void CreateTypeObject(TypeDesc typeDesc)
        {
            if (typeDesc.BaseClass != null && !_typesHeap.ContainsType(typeDesc.BaseClass.Metadata))
                throw new InvalidOperationException("Base class for this class was not loaded into domain. Load base class first");

            VTableBuilder vtb = new VTableBuilder(typeDesc, null);

            var vtBuilder = new VTableBuilder(typeDesc, null);

            if (typeDesc.BaseClass != null)
                vtBuilder.CloneBaseVTable(_typesHeap.GetTypeObject(typeDesc.BaseClass.Metadata).VTable);

            VTable vTable = vtBuilder.ExtendBaseTypeVTable().Create();


            //Add to heap         
            TypeObject typeObj = new TypeObject(typeDesc.Metadata, typeDesc, vTable);
            _typesHeap.AddTypeObject(typeDesc.Metadata, typeObj);
        }

        public TypeLoader(ITypesHeap typesHeap)
        {
            _typesHeap = typesHeap;
            //_typeObjects = new Dictionary<ClassToken, TypeObject>();
            //_globalMethods = new Dictionary<MethodToken, MethodDesc>();
        }
         
        public void LoadType(TypeDesc typeDesc)
        {
            LoadTypeAndAncestorsRecursively(typeDesc);
        }

        public void LoadGlobalMethods(List<MethodDesc> globalMethods)
        {
            foreach (var method in globalMethods)
                _typesHeap.AddGlobalMethod(method.Metadata, method);
        }
         
        public bool TypeIsLoaded(ClassToken typeMetadata)
        {
            return _typesHeap.ContainsType(typeMetadata);
        }

        public bool FirstIsOfTypeOrDerivedFromSecond(ClassToken checkingClass, ClassToken targetClass)
        {
            var checking = _typesHeap.GetTypeObject(checkingClass);
            if (checking == null)
                throw new InvalidOperationException("Checking class has not been loaded");

            return checking.TypeDesc.IsOfTypeOrDerivedFrom(targetClass);
        }
    }
}
