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
    public class LoopingAndRecursionTests: TestBase
    { 
        [TestMethod]
        public void SimpleForIterationWithIncrement()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 5;
                for (int i = 0; i < 6; i++)
                {
                    a = a + i;
                }
                return a;
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
        public void SimpleWhileIterationWithIncrement()
        {
            //Arrange
            Func<Int32> func = () =>
            {
                Int32 a = 5;
                while (a < 20)
                {
                    a = a + 1;
                }
                return a;
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
        public void CallingOneFunctionFromAnother()
        {
            //Arrange
            Func<Int32, Int32> addFive = (Int32 num) =>
            {
                Int32 r = num + 5;
                return r;
            };

            Func<Int32> mainFunc = () =>
            {
                Int32 num = 10; 
                Int32 r = addFive(num);
                return r; 
            };
             
            MethodLoader mainFuncLoader = new MethodLoader(mainFunc.GetMethodInfo());
            MethodLoader addFiveLoader = new MethodLoader(addFive.GetMethodInfo());

            MethodDesc mainFuncMethodDesc = mainFuncLoader.ConstructMethodDesc();
            MethodDesc addFiveMethodDesc = addFiveLoader.ConstructMethodDesc();

            mainFuncMethodDesc.EntryPoint().AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);
            addFiveMethodDesc.AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global);

            var executor = GetExecutionEngineForGlobalEntryPoint(mainFuncMethodDesc, addFiveMethodDesc);

            //Act
            executor.Start();

            //Assert 
            var res = mainFunc();
            Assert.IsTrue(executor.EntryPointReturn.Equals(res));
        }


        //[TestMethod]
        //public void SimpleRecursionForCalculatingFactorial()
        //{
        //    //Arrange
        //    Func<Int32> funcFactorial = () =>
        //    {
        //        Int32 a = 5;
        //        while (a < 20)
        //        {
        //            a = a + 1;
        //        }
        //        return a;
        //    };

        //    Func<Int32> funcMain = () =>
        //    {
        //        funcFactorial()
        //        Int32 a = 5;
        //        while (a < 20)
        //        {
        //            a = a + 1;
        //        }
        //        return a;
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

    }
}
