using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using ILTool.Kernel.OperationCodes;
using ILTool.TestingInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class ExecutorTests
    {
        [TestMethod]
        public void SimpleInstructions()
        {
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddArguments(new MethodArgDescription[]
                                        {
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Ldc_I4, 2),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Ldc_I4, 4),
                                          new ILInstruction(ByteCode.Stloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                          new ILInstruction(ByteCode.Mul),
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Mul)
                                        });





            CompiledModel compiledModel = new CompiledModel()
                                                .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
        }

        [TestMethod]
        public void MethodCalls()
        {
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription(){TypeToken =TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddArguments(new MethodArgDescription[]
                                        {
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Ldc_I4, 2),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Ldc_I4, 4),
                                          new ILInstruction(ByteCode.Stloc, 1),
                                          new ILInstruction(ByteCode.Call, new Metadata.MethodToken("", "Method_Void_Void", null)),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                        });




            MethodDesc method_Void_Void = new MethodDesc("Method_Void_Void", 3) 
                                                     .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                                     .AddLocals(new LocalVarDescription[]
                                                     {
                                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                                     })
                                                     .AddInstructions(new ILInstruction[]
                                                     {
                                                            new ILInstruction(ByteCode.Ldc_I4, 11),
                                                            new ILInstruction(ByteCode.Ldc_I4, 12),
                                                            new ILInstruction(ByteCode.Call, new Metadata.MethodToken("", "Method_Int32Int32_Void", null))
                                                     });

            MethodDesc method_Int32Int32_Void = new MethodDesc("Method_Int32Int32_Void", 3)
                                                        .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                                        .AddArguments(new MethodArgDescription[]
                                                        {
                                                                new MethodArgDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata },
                                                                new MethodArgDescription() {TypeToken =  TempTypeLocator.Int32Desc.Metadata },
                                                        })
                                                        .AddInstructions(new ILInstruction[]
                                                        {
                                                                new ILInstruction(ByteCode.Ldarg, 0),
                                                                new ILInstruction(ByteCode.Ldarg, 1),
                                                                new ILInstruction(ByteCode.Mul),
                                                                new ILInstruction(ByteCode.Ret)
                                                        });

            CompiledModel compiledModel = new CompiledModel()
                                            .AddMethod(mainMethod)
                                            .AddMethod(method_Void_Void)
                                            .AddMethod(method_Int32Int32_Void);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
        }

        [TestMethod]
        public void FactorialRecursion()
        {
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 1)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Ldc_I4, 8),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                          new ILInstruction(ByteCode.Call, new Metadata.MethodToken( "", "FactorialMethod", null)),
                                          new ILInstruction(ByteCode.Stloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Ret)
                                        });

            MethodDesc factorialMethod = new MethodDesc("FactorialMethod", 3)
                                            .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                                            .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                            .AddArguments(new MethodArgDescription[]
                                            {
                                                    new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            })
                                            .AddInstructions(new ILInstruction[]
                                                {
                                                    new ILInstruction(ByteCode.Ldarg, 0),
                                                    new ILInstruction(ByteCode.Ldc_I4, 2),
                                                    new ILInstruction(ByteCode.Bge, 5),  //to the 5th instruction
                                                    new ILInstruction(ByteCode.Ldc_I4, 1),
                                                    new ILInstruction(ByteCode.Ret),
                                                    new ILInstruction(ByteCode.Ldarg, 0),
                                                    new ILInstruction(ByteCode.Ldarg, 0),
                                                    new ILInstruction(ByteCode.Ldc_I4, 1),
                                                    new ILInstruction(ByteCode.Sub),
                                                    new ILInstruction(ByteCode.Call, new Metadata.MethodToken("", "FactorialMethod", null)),
                                                    new ILInstruction(ByteCode.Mul, 2),
                                                    new ILInstruction(ByteCode.Nop, "FactorialMethod before ret"),
                                                    new ILInstruction(ByteCode.Ret)
                                                });

            CompiledModel compiledModel = new CompiledModel()
                                            .AddMethod(mainMethod)
                                            .AddMethod(factorialMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.AreEqual((Int32)executor.EntryPointReturn, 40320);
        }

        [TestMethod]
        public void SimpleTryFinally()
        {
            ITest test = new CorrectOrderCheck();
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddCallback(test)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddArguments(new MethodArgDescription[]
                                        {
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Ldc_I4, 2),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Ldc_I4, 4),
                                          new ILInstruction(ByteCode.Stloc, 1),
                                          new ILInstruction(ByteCode.Nop, "1. Just before try start").Data(1),               
                                          //----------try begin-------------
                                          new ILInstruction(ByteCode.Nop, "2. At the try first instruction start").AddLabel("TryBeg").Data(2),  //5            
                                          new ILInstruction(ByteCode.Ldc_I4, 5),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Nop, "3. Just before leave instruction").Data(3),
                                          new ILInstruction(ByteCode.Leave, new Label("AfterBlock")).AddLabel("TryEnd"),     //9                              //9
                                          //----------try end-------------
                                          new ILInstruction(ByteCode.Nop, "Between try and finally"),
                                          //----------finally begin-------------
                                          new ILInstruction(ByteCode.Nop, "4. At the finally start").AddLabel("FinBeg").Data(4),                 //11
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                          new ILInstruction(ByteCode.Mul),
                                          new ILInstruction(ByteCode.Pop),
                                          new ILInstruction(ByteCode.Nop, "5. Just before endfinally instruction").Data(5),
                                          new ILInstruction(ByteCode.Endfinally).AddLabel("FinEnd"),                                  //17
                                          //----------finally end-------------
                                          new ILInstruction(ByteCode.Nop, "Just after endfinally instruction"),
                                          new ILInstruction(ByteCode.Nop, "6(end). instruction where leave points").AddLabel("AfterBlock").Data(6),         //19
                                          new ILInstruction(ByteCode.Ret)
                                        });

            mainMethod.EHTable.AddFinallyHandler("TryBeg", "TryEnd", "FinBeg", "FinEnd");


            CompiledModel compiledModel = new CompiledModel()
                                                .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(test.Success());
        }


        [TestMethod]
        public void TwoNestedTryFinallyBlocksWithinOneMethod()
        {
            ITest test = new CorrectOrderCheck();
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddCallback(test)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddArguments(new MethodArgDescription[]
                                        {
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Ldc_I4, 2),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Ldc_I4, 4),
                                          new ILInstruction(ByteCode.Stloc, 1),
                                          new ILInstruction(ByteCode.Nop, "1. Just before outer try start").Data(1),               
                                          //----------outer try begin-------------
                                          new ILInstruction(ByteCode.Nop, "2. At the outer try first instruction start").AddLabel("OuterTryBeg").Data(2),  //5            
                                          new ILInstruction(ByteCode.Ldc_I4, 5),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                              //----------inner try begin-------------
                                              new ILInstruction(ByteCode.Nop, "3. At the inner try first instruction start").AddLabel("InnerTryBeg").Data(3),  //8  
                                              new ILInstruction(ByteCode.Ldc_I4, 5),
                                              new ILInstruction(ByteCode.Stloc, 0),
                                              new ILInstruction(ByteCode.Nop, "4. Just before inner leave instruction").Data(4),
                                              new ILInstruction(ByteCode.Leave, new Label("AfterInnerBlock")).AddLabel("InnerTryEnd"),          //12
                                              //----------inner try end-------------
                                          new ILInstruction(ByteCode.Nop, "Just after inner try leave instruction"),
                                          new ILInstruction(ByteCode.Nop, "7. Where inner try's leave points to").AddLabel("AfterInnerBlock").Data(7),    //14
                                          new ILInstruction(ByteCode.Ldc_I4, 5),
                                          new ILInstruction(ByteCode.Stloc, 0),
                                          new ILInstruction(ByteCode.Nop, "8. Just before outer leave instruction").Data(8),
                                          new ILInstruction(ByteCode.Leave, new Label("AfterOuterBlock")).AddLabel("OuterTryEnd"),             //18
                                          //----------outer try end-------------
                                          new ILInstruction(ByteCode.Nop, "Between outer try and finally"),
                                          //----------outer finally begin-------------
                                          new ILInstruction(ByteCode.Nop, "9. At the outer finally start").AddLabel("OuterFinBeg").Data(9),          //20
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                          new ILInstruction(ByteCode.Mul),
                                          new ILInstruction(ByteCode.Pop),
                                          new ILInstruction(ByteCode.Nop, "10. Just before outer endfinally instruction").Data(10),
                                          new ILInstruction(ByteCode.Endfinally).AddLabel("OuterFinEnd"),                                  //26
                                          //----------outer finally end-------------
                                          new ILInstruction(ByteCode.Nop, "Just after outer endfinally instruction"),
                                          new ILInstruction(ByteCode.Nop, "11(end). instruction where outer try's leave points").AddLabel("AfterOuterBlock").Data(11),     //28
                                          new ILInstruction(ByteCode.Ret),
                                           
                                          //----------inner finally begin-------------
                                          new ILInstruction(ByteCode.Nop, "5. At the inner finally start").AddLabel("InnerFinBeg").Data(5),                 //30
                                          new ILInstruction(ByteCode.Ldloc, 1),
                                          new ILInstruction(ByteCode.Ldloc, 0),
                                          new ILInstruction(ByteCode.Mul),
                                          new ILInstruction(ByteCode.Pop),
                                          new ILInstruction(ByteCode.Nop, "6. Just before inner endfinally instruction").Data(6),
                                          new ILInstruction(ByteCode.Endfinally).AddLabel("InnerFinEnd"),              //36
                                          //----------inner finally end-------------

                                        });

            mainMethod.EHTable.AddFinallyHandler("InnerTryBeg", "InnerTryEnd", "InnerFinBeg", "InnerFinEnd");
            mainMethod.EHTable.AddFinallyHandler("OuterTryBeg", "OuterTryEnd", "OuterFinBeg", "OuterFinEnd");

            CompiledModel compiledModel = new CompiledModel()
                                                .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(test.Success());
        }



        [TestMethod]
        public void TryFinallyInsideAnotherTryFinallyBlock()
        {
            ITest test = new CorrectOrderCheck();
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint() 
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddCallback(test)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddArguments(new MethodArgDescription[]
                                        {
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                            new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                          new ILInstruction(ByteCode.Nop, "1. Outer try start").Data(1),    //0
                                          new ILInstruction(ByteCode.Nop, "2. Outer try leave instruction").Data(2),
                                          new ILInstruction(ByteCode.Leave, 13),                     //2

                                          //----------outer finally begin-------------
                                          new ILInstruction(ByteCode.Nop, "3. Outer finally start").Data(3),                 //3
                                     
                                              new ILInstruction(ByteCode.Nop, "4. Inner try first instruction start").Data(4),  //4  
                                              new ILInstruction(ByteCode.Nop, "5. Just before inner try leave instruction").Data(5),
                                              new ILInstruction(ByteCode.Leave, 11),          //6

                                              new ILInstruction(ByteCode.Nop, "Between inner try and inner finally"),

                                              new ILInstruction(ByteCode.Nop, "6. At the inner finally start").Data(6),                 //8
                                              new ILInstruction(ByteCode.Nop, "7. Just before inner endfinally instruction").Data(7),
                                              new ILInstruction(ByteCode.Endfinally),        //10
                                              
                                          new ILInstruction(ByteCode.Nop, "8. Just before outer endfinally instruction").Data(8),    //11
                                          new ILInstruction(ByteCode.Endfinally),              //12
                                          //----------outer finally end-------------

                                          new ILInstruction(ByteCode.Nop, "9(end). instruction where outer try's leave points").Data(9),      //13
                                          new ILInstruction(ByteCode.Ret)
                                        });

            mainMethod.EHTable.AddFinallyHandler(4, 6, 8, 10);
            mainMethod.EHTable.AddFinallyHandler(0, 2, 3, 12);

            CompiledModel compiledModel = new CompiledModel()
                                                .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            IExecutionEngine executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(test.Success());
        }
    }
}
