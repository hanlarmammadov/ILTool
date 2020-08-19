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
    public class GeneralIntegrationTests: TestBase
    { 
        [TestMethod]
        public void SimpleFunctionReturnsResultOfArithmeticOpers()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 40;
                Int32 b = a + 2;
                return b;
            };
                 
            MethodLoader loader = new MethodLoader(func.GetMethodInfo());
            MethodDesc methodDesc = loader.ConstructMethodDesc();
            methodDesc.EntryPoint()
                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global); 
            var executor = GetExecutionEngineForGlobalEntryPoint(methodDesc);

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(executor.EntryPointReturn.Equals(42));
        }

        [TestMethod]
        public void ArithmeticFunctionWithTwoTryFinallies()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 5;
                try
                {
                    try
                    {
                        Int32 b = a + 33;
                        Int32 c = b * 4;
                        return a + b + c;
                    }
                    finally
                    {
                        a = a + 1;
                    }
                }
                finally
                {
                    a = a * 2;
                }
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
        public void SimpleFunctionWithIfElseBranching()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 5;
                Int32 b = 3;
                Int32 result = 0;
                if (a - b > 0)
                    result = 1; 
                else
                    result = -1;
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
        public void FunctionWithMixedIntAndFloatArithmeticsAndBranching()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 5;
                Int32 b = -3;
                Int32 c = a + b;
                Single s1 = 21.6f;
                Single s2 = 33.24f; 
                Single s = s1 + s2;
                Byte bt = 32;
                if ((s == 54.84f) && (c == -8) && (true == true))
                {
                    Byte mult = 2;
                    bt = (Byte)(bt * mult);
                }

                bt = (Byte)(bt & 6);

                return (Int32)bt;
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



    }
}
