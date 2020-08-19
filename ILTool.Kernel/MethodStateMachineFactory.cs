using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap; 

namespace ILTool.Kernel
{
    public class MethodStateMachineFactory : IMethodStateMachineFactory
    {
        private IILOperationSet _ilOperationSet;
        private IGCHeap _gcHeap;
        private ITypeLoader _typeLoader;
        private ITypesHeap _typesHeap;

        public MethodStateMachineFactory(IGCHeap gcHeap, ITypesHeap typesHeap, ITypeLoader typeLoader, IILOperationSet ilOperationSet)
        {
            _ilOperationSet = ilOperationSet;
            _gcHeap = gcHeap;
            _typeLoader = typeLoader;
            _typesHeap = typesHeap;
        }

        public MethodStateMachine Create(MethodDesc methodDesc)
        {
            MethodStateMachine entryPointExecModel = MethodStateMachine.CreateForMethod(methodDesc, _gcHeap, _typesHeap, _typeLoader, _ilOperationSet);
            return entryPointExecModel;
        }
    }
}
