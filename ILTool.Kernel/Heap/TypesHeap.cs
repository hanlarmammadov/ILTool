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
    public class TypesHeap : ITypesHeap
    {
        private Dictionary<ClassToken, TypeObject> _typeObjects;
        private Dictionary<MethodToken, MethodDesc> _globalMethods;

        public TypesHeap()
        {
            _typeObjects = new Dictionary<ClassToken, TypeObject>();
            _globalMethods = new Dictionary<MethodToken, MethodDesc>();
        }

        public TypeObject GetTypeObject(ClassToken token)
        {
            TypeObject result = null;
            _typeObjects.TryGetValue(token, out result);
            return result;
        }

        public void AddTypeObject(ClassToken token, TypeObject typeObject)
        {
            _typeObjects.Add(token, typeObject);
        }


        public MethodDesc GetGlobalMethod(MethodToken token)
        {
            MethodDesc result = null;
            _globalMethods.TryGetValue(token, out result);
            return result;
        }

        public void AddGlobalMethod(MethodToken token, MethodDesc methodDesc)
        {
            _globalMethods.Add(token, methodDesc);
        }

        public bool ContainsType(ClassToken token)
        {
            return _typeObjects.ContainsKey(token);
        }

        public class Factory : ITypesHeapFactory
        {
            public ITypesHeap Create()
            {
                TypesHeap typesHeap = new TypesHeap();
                return typesHeap;
            }
        }

    }
}
