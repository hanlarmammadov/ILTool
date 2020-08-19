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
    public class DomainModel
    {
        private IGCHeap _gcHeap;
        private ITypesHeap _typesHeap;
        private ITypeLoader _typeLoader;

        public IGCHeap GCHeap
        {
            get
            {
                return _gcHeap;
            }
        }

        public ITypesHeap TypesHeap
        {
            get
            {
                return _typesHeap;
            }
        }

        public DomainModel(IGCHeapFactory gcHeapFactory, ITypesHeapFactory typesHeapFactory)
        {
            _gcHeap = gcHeapFactory.Create();
            _typesHeap = typesHeapFactory.Create();
            _typeLoader = new TypeLoader(_typesHeap);
        }

        //public TypeObject GetTypeObject(ClassToken typeMetadata)
        //{
        //    return _typeLoader.GetTypeObject(typeMetadata); 
        //}
        
        public void LoadType(TypeDesc typeDesc)
        {
            _typeLoader.LoadType(typeDesc);
        }

        public IExecutionEngine GetExecutor(CompiledModel compiledModel, IILOperationSet ilOperationSet)
        {
            _typeLoader.LoadGlobalMethods(compiledModel.Methods.Values.ToList());
            IExecutionEngine executor = new ExecutionEngine(compiledModel, _gcHeap, _typesHeap, _typeLoader, new CallStack(Int16.MaxValue), ilOperationSet);
            return executor;
        }
    } 
}
