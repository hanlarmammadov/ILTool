using System;
using System.Collections.Generic;
using FluentAssertions;
using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.Engines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class AddEngineTests
    {
        //[TestMethod]
        //public void StackWith2ValidInt32ValuesExecuteMethodSucceeds()
        //{
        //    //Arrange            
        //    Int32 val1 = 42;
        //    Int32 val2 = 24;
        //    Stack<object> evalStack = new Stack<object>();
        //    evalStack.Push(val1);
        //    evalStack.Push(val2);
        //    AddEngine engine = new AddEngine(evalStack);
             
        //    //Act
        //    engine.Execute();

        //    //Assert
        //    evalStack.Count.Should().Be(1);
        //    evalStack.Peek().Should().Be(val1+ val2);
        //}

        //[TestMethod]
        //public void StackWith1ValidInt32ValueExecuteMethodThrows()
        //{
        //    //Arrange            
        //    Int32 val1 = 42; 
        //    Stack<object> evalStack = new Stack<object>();
        //    evalStack.Push(val1); 
        //    AddEngine engine = new AddEngine(evalStack);
 
        //    //Assert
        //    Assert.ThrowsException<InsufficientStackSizeException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    });
        //}

        //[TestMethod]
        //public void StackWith0ValidInt32ValueExecuteMethodThrows()
        //{
        //    //Arrange           
        //    Stack<object> evalStack = new Stack<object>(); 
        //    AddEngine engine = new AddEngine(evalStack);

        //    //Assert
        //    Assert.ThrowsException<InsufficientStackSizeException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    }); 
        //}

        //[TestMethod]
        //public void StackWith2InvalidValuesExecuteMethodThrows()
        //{
        //    //Arrange            
        //    string val1 = "42";
        //    string val2 = "24";
        //    Stack<object> evalStack = new Stack<object>();
        //    evalStack.Push(val1);
        //    evalStack.Push(val2);
        //    AddEngine engine = new AddEngine(evalStack);

        //    //Assert
        //    Assert.ThrowsException<OperandsNotSupportedByOperationException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    });
        //}

        //[TestMethod]
        //public void StackWith1InvalidValueExecuteMethodThrows1()
        //{
        //    //Arrange            
        //    Int32 val1 = 42;
        //    string val2 = "24";
        //    Stack<object> evalStack = new Stack<object>();
        //    evalStack.Push(val1);
        //    evalStack.Push(val2);
        //    AddEngine engine = new AddEngine(evalStack);

        //    //Assert
        //    Assert.ThrowsException<OperandsNotSupportedByOperationException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    }); 
        //}

        //[TestMethod]
        //public void StackWith1InvalidValueExecuteMethodThrows2()
        //{
        //    //Arrange            
        //    string val1 = "42";
        //    Int32 val2 = 24;
        //    Stack<object> evalStack = new Stack<object>();
        //    evalStack.Push(val1);
        //    evalStack.Push(val2);
        //    AddEngine engine = new AddEngine(evalStack);

        //    //Assert
        //    Assert.ThrowsException<OperandsNotSupportedByOperationException>(() =>
        //    {
        //        //Act
        //        engine.Execute();
        //    });
        //}
    }
}
