using System;
using System.Collections.Generic;
using FluentAssertions;
using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.Engines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class LoadConstI4EngineTests
    {
        //[TestMethod]
        //public void ValidInt32ToExecuteMethod()
        //{
        //    //Arrange
        //    Stack<object> evalStack = new Stack<object>();
        //    LoadConstI4Engine engine = new LoadConstI4Engine(evalStack);
        //    Int32 val = 42;

        //    //Act
        //    engine.Execute(val);

        //    //Assert
        //    evalStack.Count.Should().Be(1);
        //    evalStack.Peek().Should().Be(42); 
        //}

        //[TestMethod]
        //public void CallingExecuteMethodWithoutParamsShouldThrow()
        //{
        //    //Arrange
        //    Stack<object> evalStack = new Stack<object>();
        //    LoadConstI4Engine engine = new LoadConstI4Engine(evalStack);
             
        //    //Assert
        //    Assert.ThrowsException<OperandsNotSupportedByOperationException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    }); 
        //}

        //[TestMethod]
        //public void CallingExecuteMethodWithNonInt32ParamShouldThrow()
        //{
        //    //Arrange
        //    Stack<object> evalStack = new Stack<object>();
        //    LoadConstI4Engine engine = new LoadConstI4Engine(evalStack);
        //    string val = "42";

        //    //Assert
        //    Assert.ThrowsException<OperandsNotSupportedByOperationException>(() =>
        //    {
        //        //Act
        //        engine.Execute(val);
        //    });
        //}
    }
}
