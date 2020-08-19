using System;
using System.Reflection;
using ILTool.Kernel;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Loader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Tests
{
    [TestClass]
    public class Int32PrimitiveOperationsTests : TestBase
    { 

        #region Addition

        [TestMethod]
        public void Int32_Simple_Adding_Positive_Positive()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 435;
                Int32 b = 765;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Positive_Zero()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 435;
                Int32 b = 0;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Zero_Zero()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 0;
                Int32 b = 0;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Positive_Negative()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 23;
                Int32 b = -889;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Negative_Negative()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = -23;
                Int32 b = -889;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Zero_Negative()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 0;
                Int32 b = -889;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Adding_Positive_Overflow()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 2147483647;
                Int32 b = 1;
                Int32 result = a + b;
                return result;
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

        #region Subtraction

        [TestMethod]
        public void Int32_Simple_Subtraction_Positive_Positive_Zero_Result()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 935453;
                Int32 b = 935453;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Subtraction_Positive_Positive_Negative_Result()
        {
            //Arrange
            Func<Int32> func = () =>
            { 
                Int32 a = 935453;
                Int32 b = 1134532;
                Int32 result = a + b;
                return result;
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
        public void Int32_Simple_Subtraction_Negative_Positive_Negative_Result()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = -935453;
                Int32 b = 1134532;
                Int32 result = a + b;
                return result;
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

        #region Multiplication

        [TestMethod]
        public void Int32_Simple_Multiplication_Positive_Positive()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 264;
                Int32 b = 185;
                Int32 result = a * b;
                return result;
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
        public void Int32_Simple_Multiplication_Zero_Positive()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 0;
                Int32 b = 935453;
                Int32 result = a * b;
                return result;
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
        public void Int32_Simple_Multiplication_Negative_Positive()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = -564;
                Int32 b = 935453;
                Int32 result = a * b;
                return result;
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
        public void Int32_Simple_Multiplication_Positive_Overflow()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 2147483647;
                Int32 b = 3;
                Int32 result = a * b;
                return result;
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
        public void Int32_Simple_Multiplication_Negative_Overflow()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = -2147483648;
                Int32 b = 3;
                Int32 result = a * b;
                return result;
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

        #region And operation

        [TestMethod]
        public void Int32_Simple_And_Operation()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 264;
                Int32 b = 185;
                Int32 result = a & b;
                return result;
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

        #region Or operation

        [TestMethod]
        public void Int32_Simple_Or_Operation()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 264;
                Int32 b = 185;
                Int32 result = a | b;
                return result;
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

        #region Xor operation

        [TestMethod]
        public void Int32_Simple_Xor_Operation()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 264;
                Int32 b = 185;
                Int32 result = a ^ b;
                return result;
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

        #region Equal operation

        [TestMethod]
        public void Int32_Simple_Equals_Operation_False()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 264;
                Int32 b = 185;
                Boolean result = a == b;
                return result;
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
        public void Int32_Simple_Equals_Operation_False2()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = -264;
                Int32 b = 264;
                Boolean result = a == b;
                return result;
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
        public void Int32_Simple_Equals_Operation_True()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 65453;
                Int32 b = 65453;
                Boolean result = a == b;
                return result;
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

        #region NotEqual operation

        [TestMethod]
        public void Int32_Simple_NotEquals_Operation_False()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 65453;
                Int32 b = 65453;
                Boolean result = a != b;
                return result;
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
        public void Int32_Simple_NotEquals_Operation_True()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = -65453;
                Int32 b = 65453;
                Boolean result = a != b;
                return result;
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

        #region Greater operation

        [TestMethod]
        public void Int32_Simple_Greater_Operation_False()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 22;
                Int32 b = 42;
                Boolean result = a > b;
                return result;
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
        public void Int32_Simple_Greater_Operation_False2()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 42;
                Int32 b = 42;
                Boolean result = a > b;
                return result;
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
        public void Int32_Simple_Greater_Operation_True()
        {
            //Arrange
            Func<Boolean> func = () =>
            {
                Int32 a = 42;
                Int32 b = 22;
                Boolean result = a > b;
                return result;
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

        #region GreaterOrEqual operation


        #endregion

        #region Less operation


        #endregion

        #region LessOrEqual operation


        #endregion


    }
}
