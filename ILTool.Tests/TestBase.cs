using ILTool.Kernel;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Tests
{
    public abstract class TestBase
    {
        protected IExecutionEngine GetExecutionEngineForGlobalEntryPoint(params MethodDesc[] globalMethods)
        {

            CompiledModel compiledModel = new CompiledModel();
            foreach(MethodDesc method in globalMethods)
                compiledModel.AddMethod(method);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            domainModel.LoadType(TempTypeLocator.UInt32Desc);
            domainModel.LoadType(TempTypeLocator.BooleanDesc);
            domainModel.LoadType(TempTypeLocator.Int16Desc);
            domainModel.LoadType(TempTypeLocator.UInt16Desc);
            domainModel.LoadType(TempTypeLocator.Int64Desc);
            domainModel.LoadType(TempTypeLocator.UInt64Desc);
            domainModel.LoadType(TempTypeLocator.ByteDesc);
            domainModel.LoadType(TempTypeLocator.SByteDesc);
            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());
            return executor;
        }
    }
}
