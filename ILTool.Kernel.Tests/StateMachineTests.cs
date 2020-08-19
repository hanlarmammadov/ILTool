using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using FluentAssertions;
using ILTool.Kernel.OperationCodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Kernel.Tests
{
    //[TestClass]
    //public class StateMachineTests
    //{
    //    [TestMethod]
    //    public void CallingExecuteWith3ILOperationsAndValidOperandsSucceeds()
    //    {
    //        //Arrange
    //        MethodContext methodEnvironment = new MethodContext()
    //        {
    //            Arguments = null,
    //            EvalStack = new Stack<object>(),
    //            LocalVars = new LocalVar[1]
    //        };
    //        StateMachine stateMachine = new StateMachine(methodEnvironment, new ILOperationSet());

    //        //Act
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 4));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 6));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Add));

    //        //Assert
    //        methodEnvironment.EvalStack.Count.Should().Be(1);
    //        methodEnvironment.EvalStack.Peek().Should().Be(10);
    //    }

    //    [TestMethod]
    //    public void CallingExecuteWith5ILOperationsAndValidOperandsSucceeds()
    //    {
    //        //Arrange 
    //        MethodContext methodEnvironment = new MethodContext()
    //        {
    //            Arguments = null,
    //            EvalStack = new Stack<object>(),
    //            LocalVars = new LocalVar[1]
    //        };
    //        StateMachine stateMachine = new StateMachine(methodEnvironment, new ILOperationSet());

    //        //Act
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 1));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 2));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 3));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 4));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Add));

    //        //Assert
    //        methodEnvironment.EvalStack.Count.Should().Be(3);
    //        methodEnvironment.EvalStack.Peek().Should().Be(7);
    //    }

    //    [TestMethod]
    //    public void CallingExecuteWith5ILOperationsAndValidOperandsSucceeds2()
    //    {
    //        //Arrange
    //        MethodContext methodEnvironment = new MethodContext()
    //        {
    //            Arguments = null,
    //            EvalStack = new Stack<object>(),
    //            LocalVars = new LocalVar[1]
    //        };
    //        StateMachine stateMachine = new StateMachine(methodEnvironment, new ILOperationSet());

    //        //Act
    //        //6*4+2
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 2));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 4));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 6));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Mul));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Add));

    //        //Assert
    //        methodEnvironment.EvalStack.Count.Should().Be(1);
    //        methodEnvironment.EvalStack.Peek().Should().Be(26);
    //    }

    //    [TestMethod]
    //    public void CallingExecuteWith9ILOperationsAndValidOperandsSucceeds2()
    //    {
    //        //Arrange
    //        MethodContext methodEnvironment = new MethodContext()
    //        {
    //            Arguments = null,
    //            EvalStack = new Stack<object>(),
    //            LocalVars = new LocalVar[2]
    //            {
    //                new LocalVar(){ Description = new LocalVarDescription(){  Type = typeof(Int32) } },
    //                new LocalVar(){ Description = new LocalVarDescription(){  Type = typeof(Int32) } }
    //            }
    //        };
    //        StateMachine stateMachine = new StateMachine(methodEnvironment, new ILOperationSet());

    //        //Act
    //        //2*4*4 = 32
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 2));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Stloc, 0));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldc_I4, 4));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Stloc, 1));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldloc, 1));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldloc, 0));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Mul));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Ldloc, 1));
    //        stateMachine.Execute(new ILInstruction(OpCodes.Mul));

    //        //Assert
    //        methodEnvironment.EvalStack.Count.Should().Be(1);
    //        methodEnvironment.EvalStack.Peek().Should().Be(32);
    //    }
    //}
}
