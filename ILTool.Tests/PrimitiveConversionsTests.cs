using System;
using System.Reflection;
using System.Reflection.Emit;
using ILTool.Kernel;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Tests
{
    [TestClass]
    public class PrimitiveConversionsTests : TestBase
    { 

        //[TestMethod]
        //public void Convert_Byte_To_Int16()
        //{
        //    //Arrange
        //    var dynMethod = new DynamicMethod("Func", typeof(int), new Type[0], typeof(PrimitiveConversionsTests).Module);

        //    var il = dynMethod.GetILGenerator();
        //    il.DeclareLocal(typeof(Byte));   //source
        //    il.DeclareLocal(typeof(SByte));  //target

        //    il.Emit(OpCodes.Ldc_I4, 250);
        //    il.Emit(OpCodes.Stloc_0);
        //    il.Emit(OpCodes.Ldloc_0);
        //    il.Emit(OpCodes.Conv_I1);
        //    il.Emit(OpCodes.Stloc_1);
        //    il.Emit(OpCodes.Ldloc_1);
        //    il.Emit(OpCodes.Ret);

        //    Func<int> method = (Func<int>)dynMethod.CreateDelegate(typeof(Func<int>));

        //    MethodLoader loader = new MethodLoader(method.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = method();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}


        #region From UInt64

        [TestMethod]
        public void Convert_UInt64_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                UInt64 source = (UInt64)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_UInt32()
        {
            //Arrange
            Func<UInt32> func = () =>
            {
                UInt64 source = (UInt64)2500;
                UInt32 target = (UInt32)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_Int32()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                UInt64 source = (UInt64)2500;
                Int32 target = (Int32)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_UInt16()
        {
            //Arrange
            Func<UInt16> func = () =>
            {
                UInt64 source = (UInt64)2500;
                UInt16 target = (UInt16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_Int16()
        {
            //Arrange
            Func<Int16> func = () =>
            {
                UInt64 source = (UInt64)2500;
                Int16 target = (Int16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                UInt64 source = (UInt64)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt64_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                UInt64 source = (UInt64)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From Int64

        [TestMethod]
        public void Convert_Int64_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                Int64 source = (Int64)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_UInt32()
        {
            //Arrange
            Func<UInt32> func = () =>
            {
                Int64 source = (Int64)2500;
                UInt32 target = (UInt32)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_Int32()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int64 source = (Int64)2500;
                Int32 target = (Int32)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_UInt16()
        {
            //Arrange
            Func<UInt16> func = () =>
            {
                Int64 source = (Int64)2500;
                UInt16 target = (UInt16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_Int16()
        {
            //Arrange
            Func<Int16> func = () =>
            {
                Int64 source = (Int64)2500;
                Int16 target = (Int16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                Int64 source = (Int64)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int64_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                Int64 source = (Int64)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From UInt32

        [TestMethod]
        public void Convert_UInt32_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                UInt32 source = (UInt32)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_UInt32_To_UInt32()
        //{
        //    //Arrange
        //    Func<UInt32> func = () =>
        //    {
        //        UInt32 source = (UInt32)2500;
        //        UInt32 target = (UInt32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_UInt32_To_Int32()
        //{
        //    //Arrange
        //    Func<Int32> func = () =>
        //    {
        //        UInt32 source = (UInt32)2500;
        //        Int32 target = (Int32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_UInt32_To_UInt16()
        {
            //Arrange
            Func<UInt16> func = () =>
            {
                UInt32 source = (UInt32)2500;
                UInt16 target = (UInt16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt32_To_Int16()
        {
            //Arrange
            Func<Int16> func = () =>
            {
                UInt32 source = (UInt32)2500;
                Int16 target = (Int16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt32_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                UInt32 source = (UInt32)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt32_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                UInt32 source = (UInt32)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From Int32

        [TestMethod]
        public void Convert_Int32_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                Int32 source = (Int32)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_Int32_To_UInt32()
        //{
        //    //Arrange
        //    Func<UInt32> func = () =>
        //    {
        //        Int32 source = (Int32)2500;
        //        UInt32 target = (UInt32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Int32_To_Int32()
        //{
        //    //Arrange
        //    Func<Int32> func = () =>
        //    {
        //        Int32 source = (Int32)2500;
        //        Int32 target = (Int32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_Int32_To_UInt16()
        {
            //Arrange
            Func<UInt16> func = () =>
            {
                Int32 source = (Int32)2500;
                UInt16 target = (UInt16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int32_To_Int16()
        {
            //Arrange
            Func<Int16> func = () =>
            {
                Int32 source = (Int32)2500;
                Int16 target = (Int16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int32_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                Int32 source = (Int32)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int32_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                Int32 source = (Int32)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From UInt16

        [TestMethod]
        public void Convert_UInt16_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                UInt16 source = (UInt16)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_UInt16_To_UInt32()
        //{
        //    //Arrange
        //    Func<UInt32> func = () =>
        //    {
        //        UInt16 source = (UInt16)2500;
        //        UInt32 target = (UInt32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_UInt16_To_Int32()
        //{
        //    //Arrange
        //    Func<Int32> func = () =>
        //    {
        //        UInt16 source = (UInt16)2500;
        //        Int32 target = (Int32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_UInt16_To_UInt16()
        //{
        //    //Arrange
        //    Func<UInt16> func = () =>
        //    {
        //        UInt16 source = (UInt16)2500;
        //        UInt16 target = (UInt16)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_UInt16_To_Int16()
        {
            //Arrange
            Func<Int16> func = () =>
            {
                UInt16 source = (UInt16)2500;
                Int16 target = (Int16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt16_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                UInt16 source = (UInt16)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_UInt16_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                UInt16 source = (UInt16)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From Int16

        [TestMethod]
        public void Convert_Int16_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                Int16 source = (Int16)2500;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_Int16_To_UInt32()
        //{
        //    //Arrange
        //    Func<UInt32> func = () =>
        //    {
        //        Int16 source = (Int16)(-2500);
        //        UInt32 target = (UInt32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Int16_To_Int32()
        //{
        //    //Arrange
        //    Func<Int32> func = () =>
        //    {
        //        Int16 source = (Int16)2500;
        //        Int32 target = (Int32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_Int16_To_UInt16()
        {
            //Arrange
            Func<UInt16> func = () =>
            {
                Int16 source = (Int16)2500;
                UInt16 target = (UInt16)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_Int16_To_Int16()
        //{
        //    //Arrange
        //    Func<Int16> func = () =>
        //    {
        //        Int16 source = (Int16)2500;
        //        Int16 target = (Int16)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_Int16_To_Byte()
        {
            //Arrange
            Func<Byte> func = () =>
            {
                Int16 source = (Int16)2500;
                Byte target = (Byte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        [TestMethod]
        public void Convert_Int16_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                Int16 source = (Int16)2500;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From Byte

        [TestMethod]
        public void Convert_Byte_To_Int64()
        {
            //Arrange
            Func<Int64> func = () =>
            {
                Byte source = (Byte)250;
                Int64 target = (Int64)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        //[TestMethod]
        //public void Convert_Byte_To_UInt32()
        //{
        //    //Arrange
        //    Func<UInt32> func = () =>
        //    {
        //        Byte source = (Byte)250;
        //        UInt32 target = (UInt32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Byte_To_Int32()
        //{
        //    //Arrange
        //    Func<Int32> func = () =>
        //    {
        //        Byte source = (Byte)250;
        //        Int32 target = (Int32)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Byte_To_UInt16()
        //{
        //    //Arrange
        //    Func<UInt16> func = () =>
        //    {
        //        Byte source = (Byte)250;
        //        UInt16 target = (UInt16)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Byte_To_Int16()
        //{
        //    //Arrange
        //    Func<Int16> func = () =>
        //    {
        //        Byte source = (Byte)250;
        //        Int16 target = (Int16)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        //[TestMethod]
        //public void Convert_Byte_To_Byte()
        //{
        //    //Arrange
        //    Func<Byte> func = () =>
        //    {
        //        Byte source = (Byte)250;
        //        Byte target = (Byte)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        [TestMethod]
        public void Convert_Byte_To_SByte()
        {
            //Arrange
            Func<SByte> func = () =>
            {
                Byte source = (Byte)250;
                SByte target = (SByte)source;
                return target;
            };

            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = func();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }

        #endregion

        #region From SByte
        
        //[TestMethod]
        //public void Convert_SByte_To_SByte()
        //{
        //    //Arrange
        //    Func<SByte> func = () =>
        //    {
        //        SByte source = (SByte)17;
        //        SByte target = (SByte)source;
        //        return target;
        //    };

        //    MethodLoader loader = new MethodLoader(func.GetMethodInfo());
        //    MethodDesc methodDesc = loader.ConstructMethodDesc();
        //    methodDesc.EntryPoint()
        //              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
        //    var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

        //    //Act
        //    executor.Start();

        //    //Assert 
        //    var res = func();
        //    Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        //}

        #endregion
    }
}
