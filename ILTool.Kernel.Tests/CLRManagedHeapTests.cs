using System;
using System.Collections.Generic;
using FluentAssertions;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class CLRManagedHeapTests
    {
        [TestMethod]
        public void AllocateObjectInHeapAndGetIt()
        {
            ////var m = new Moq.Mock<Dictionary<TypeDesc, HeapObj>>();
            ////m.Setup(x => x.ContainsKey(It.Is<TypeDesc>(t => t == TempTypeLocator.Int32Desc))).Returns(true);

            ////Arrange 
            //var loadedTypes = new Dictionary<TypeDesc, HeapObj>();
            //loadedTypes.Add(TempTypeLocator.Int32Desc, new HeapObj() { Val = TempTypeLocator.Int32Desc, Addr = 1001 }); 
            //IManagedHeap heap = new CLRManagedHeap.Factory().Create();

            //HeapObj int32HeapObj = heap.AllocObjType();
            //TypeObject int32TypeObj = new TypeObject(TempTypeLocator.Int32Desc, )
            //int32HeapObj.Val = new TypeObject()
            ////Act
            //HeapObj allocObj = heap.AllocObj();
            //allocObj.Val = 42;
            //HeapObj gotObj = heap.GetObj(allocObj.Addr);

            ////Assert
            //allocObj.Should().NotBeNull();
            //gotObj.Should().NotBeNull();
            //gotObj.Addr.Should().Be(allocObj.Addr);
            //gotObj.Val.Should().Be(allocObj.Val);
        }
    }
}
