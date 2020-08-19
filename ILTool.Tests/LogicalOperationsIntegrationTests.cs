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
    public class LogicalOperationsIntegrationTests : TestBase
    { 

        [TestMethod]
        public void Simple_AND_Branching_True_True()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = true;
                Boolean b = true;
                Int32 result = 0;
                if (a && b)
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
        public void Simple_AND_Branching_True_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = true;
                Boolean b = false;
                Int32 result = 0;
                if (a && b)
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
        public void Simple_AND_Branching_False_True()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = false;
                Boolean b = true;
                Int32 result = 0;
                if (a && b)
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
        public void Simple_AND_Branching_False_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = false;
                Boolean b = false;
                Int32 result = 0;
                if (a && b)
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
        public void Simple_OR_Branching_True_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = true;
                Boolean b = false;
                Int32 result = 0;
                if (a || b)
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
        public void Simple_OR_Branching_False_True()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = false;
                Boolean b = true;
                Int32 result = 0;
                if (a || b)
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
        public void Simple_OR_Branching_True_True()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = true;
                Boolean b = true;
                Int32 result = 0;
                if (a || b)
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
        public void Simple_OR_Branching_False_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = false;
                Boolean b = false;
                Int32 result = 0;
                if (a || b)
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
        public void Simple_OR_Branching_Inline_False_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean b = false;
                Int32 result = 0;
                if (false || b)
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
        public void Simple_OR_Branching_False_Inline_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Boolean a = false;
                Int32 result = 0;
                if (a || false)
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
        public void Simple_OR_Branching_Inline_False_Inline_False()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 result = 0;
                if (false || false)
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
        public void Simple_OR_Branching_Inline_True_Inline_True()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 result = 0;
                if (true || true)
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
    }
}
