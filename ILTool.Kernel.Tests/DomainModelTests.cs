using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILTool.Kernel.Tests.Helpers;
using ILTool.Kernel.Descriptions;
using System.Reflection.Emit;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using ILTool.TestingInfrastructure;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class DomainModelTests
    {
        [TestMethod]
        public void DomainModelBaseBehavior()
        {
            //Arrange  
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global) 
                                        .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                                        .AddLocals(new LocalVarDescription[]
                                        {
                                            new LocalVarDescription() {TypeToken = TempTypeLocator.Int32Desc.Metadata},
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Ldc_I4, 2),
                                            new ILInstruction(ByteCode.Stloc, 0),
                                            new ILInstruction(ByteCode.Ldloc, 0),
                                            new ILInstruction(ByteCode.Box),
                                            new ILInstruction(ByteCode.Ret),
                                        });

            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(mainMethod);


            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 

        }


        [TestMethod]
        public void CreateSimpleObjectWithCtorAndCallItsMethod()
        {
            //Arrange  
            var myObj = new TypeDesc()
            {
                Name = "MyNamespace.MyObject",
                IsPrimitive = false,
                IsValueType = false,
                BaseClass = TempTypeLocator.ObjectDesc
            };
            myObj.Methods.Add(new MethodDesc(".ctor()", 1)
                               .BelongsTo(myObj)
                               .AddPropertiesFlag(MethodOtherProps.SpecialName)
                               .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                               .AddArguments(new MethodArgDescription[]
                                   {
                                            new MethodArgDescription(){TypeToken = myObj.Metadata, SType= ESSlotType.HORef /*this*/},
                                   })
                               .AddInstructions(new ILInstruction[]
                                   {
                                            new ILInstruction(ByteCode.Ldarg, 0),
                                            new ILInstruction(ByteCode.Ret)
                                   }));

            myObj.Methods.Add(new MethodDesc("Int32 ManipulateNewObj()", 1)
                                  .BelongsTo(myObj)
                                  .AddReturnType(TempTypeLocator.Int32Desc.Metadata)
                                  .AddArguments(new MethodArgDescription[]
                                      {
                                            new MethodArgDescription(){TypeToken = myObj.Metadata, SType= ESSlotType.HORef /*this*/},
                                      })
                                  .AddLocals(new LocalVarDescription[]
                                      {
                                            new LocalVarDescription() {TypeToken = myObj.Metadata},
                                      })
                                  .AddInstructions(new ILInstruction[]
                                      {
                                            new ILInstruction(ByteCode.Ldarg, 0),
                                            new ILInstruction(ByteCode.Stloc, 0),
                                            new ILInstruction(ByteCode.Ldloc, 0),
                                            new ILInstruction(ByteCode.Ret)
                                      }));


            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddLocals(new LocalVarDescription[]
                                            {
                                                new LocalVarDescription() {TypeToken = myObj.Metadata, SType = ESSlotType.HORef },
                                            })
                                        .AddInstructions(new ILInstruction[]
                                            {
                                                new ILInstruction(ByteCode.Nop),
                                                new ILInstruction(ByteCode.Newobj, myObj.Methods[0].Metadata /*.ctor*/),
                                                new ILInstruction(ByteCode.Callvirt, myObj.Methods[1].Metadata /*ManipulateNewObj*/),
                                                new ILInstruction(ByteCode.Stloc, 0),
                                                new ILInstruction(ByteCode.Ret),
                                            });

            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(mainMethod);


            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.Int32Desc);
            domainModel.LoadType(myObj);
            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 

        }

        [TestMethod]
        public void SimpleTryCatchBlockWithinOneMethod()
        {
            ITest test = new CorrectOrderCheck();
            //Arrange
            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Nop, "1. Just before try start").Data(1),               
                                            //----------try begin-------------
                                            new ILInstruction(ByteCode.Nop, "2. At the try first instruction start").AddLabel("TryBeg").Data(2),
                                            new ILInstruction(ByteCode.Nop, "3. Just before throw").Data(3),
                                            new ILInstruction(ByteCode.Newobj, TempTypeLocator.ExceptionDesc.Methods[0].Metadata /*ctor*/),
                                            new ILInstruction(ByteCode.Throw),
                                            new ILInstruction(ByteCode.Nop, "Just after throw"),
                                            new ILInstruction(ByteCode.Nop, "Just before try leave instruction"),
                                            new ILInstruction(ByteCode.Leave, new Label("TryLeaveDest")).AddLabel("TryEnd"),                               
                                            //----------try end-------------
                                            new ILInstruction(ByteCode.Nop, "Between try and catch"),
                                            //----------catch begin-------------
                                            new ILInstruction(ByteCode.Nop, "4. At the catch start").AddLabel("CatchBeg").Data(4),
                                            new ILInstruction(ByteCode.Pop),
                                            new ILInstruction(ByteCode.Nop, "5. Just before catch leave instruction").Data(5),
                                            new ILInstruction(ByteCode.Leave, new Label("CatchLeaveDest")).AddLabel("CatchEnd"),                            
                                            //----------catch end-------------
                                            new ILInstruction(ByteCode.Nop, "Instruction where try leave points").AddLabel("TryLeaveDest"),
                                            new ILInstruction(ByteCode.Nop, "6(end). Instruction where catch leave points").AddLabel("CatchLeaveDest").Data(6),
                                            new ILInstruction(ByteCode.Ret)
                                        });

            mainMethod.EHTable.AddCatchHandler("TryBeg", "TryEnd", TempTypeLocator.ExceptionDesc.Metadata, "CatchBeg", "CatchEnd");


            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.ObjectDesc);
            domainModel.LoadType(TempTypeLocator.ExceptionDesc);
            domainModel.LoadType(TempTypeLocator.InvalidOperationExceptionDesc);

            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(test.Success());
        }

        [TestMethod]
        public void SimpleTryBlockWithinOneMethodWithTwoCatchHandlers()
        {
            //Arrange 
            ITest test = new CorrectOrderCheck();

            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Nop, "1. Just before try start").Data(1),               
                                            //----------try begin-------------
                                            new ILInstruction(ByteCode.Nop, "2. At the try first instruction start").AddLabel("TryBeg").Data(2),
                                            new ILInstruction(ByteCode.Nop, "3. Just before throw").Data(3),
                                            new ILInstruction(ByteCode.Newobj, TempTypeLocator.InvalidOperationExceptionDesc.Methods[0].Metadata /*ctor*/),
                                            new ILInstruction(ByteCode.Throw),
                                            new ILInstruction(ByteCode.Nop, "Just after throw"),
                                            new ILInstruction(ByteCode.Nop, "Just before try leave instruction"),
                                            new ILInstruction(ByteCode.Leave, new Label("TryLeaveDest")).AddLabel("TryEnd"),                               
                                            //----------try end-------------
                                            new ILInstruction(ByteCode.Nop, "Between try and catch"),
                                            //----------catch1 begin------------- 
                                            new ILInstruction(ByteCode.Nop, "4. At the catch1 start").AddLabel("Catch1Beg").Data(4),
                                            new ILInstruction(ByteCode.Pop),
                                            new ILInstruction(ByteCode.Nop, "5. Just before catch1 leave instruction").Data(5),
                                            new ILInstruction(ByteCode.Leave, new Label("CatchLeaveDest")).AddLabel("Catch1End"),                            
                                            //----------catch1 end-------------

                                            //----------catch2 begin-------------
                                            new ILInstruction(ByteCode.Pop).AddLabel("Catch2Beg"),
                                            new ILInstruction(ByteCode.Nop, "At the catch2 start"),
                                            new ILInstruction(ByteCode.Nop, "Just before catch2 leave instruction"),
                                            new ILInstruction(ByteCode.Leave, new Label("CatchLeaveDest")).AddLabel("Catch2End"),                            
                                            //----------catch2 end-------------
                                            new ILInstruction(ByteCode.Nop, "Instruction where try leave points").AddLabel("TryLeaveDest"),
                                            new ILInstruction(ByteCode.Nop, "6(end). Instruction where catch leave points").AddLabel("CatchLeaveDest").Data(6),
                                            new ILInstruction(ByteCode.Ret)
                                        });

            mainMethod.EHTable.AddCatchHandler("TryBeg", "TryEnd", new ClassToken("System.InvalidOperationException", false), "Catch1Beg", "Catch1End");
            mainMethod.EHTable.AddCatchHandler("TryBeg", "TryEnd", new ClassToken("System.Exception", false), "Catch2Beg", "Catch2End");


            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.ObjectDesc);
            domainModel.LoadType(TempTypeLocator.ExceptionDesc);
            domainModel.LoadType(TempTypeLocator.InvalidOperationExceptionDesc);

            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert 
            Assert.IsTrue(test.Success());
        }

        [TestMethod]
        public void SimpleTryCatchAndTryFinallyHandlersWithinOneMethod()
        {
            //Arrange
            ITest test = new CorrectOrderCheck();

            MethodDesc mainMethod = new MethodDesc("Main", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Nop, "1. Just before outer try start").Data(1),               
                                            //----------outer try begin-------------
                                            new ILInstruction(ByteCode.Nop, "2. At the outer try first instruction").AddLabel("OuterTryBeg").Data(2),
                                                
                                                //----------inner try begin-------------
                                                new ILInstruction(ByteCode.Nop, "3. At the inner try first instruction").AddLabel("InnerTryBeg").Data(3),
                                                new ILInstruction(ByteCode.Nop, "4. Just before throw").Data(4),
                                                new ILInstruction(ByteCode.Newobj, TempTypeLocator.ExceptionDesc.Methods[0].Metadata /*ctor*/),
                                                new ILInstruction(ByteCode.Throw),
                                                new ILInstruction(ByteCode.Nop, "Just after throw"),
                                                new ILInstruction(ByteCode.Leave, new Label("AfterInnerBlock")).AddLabel("InnerTryEnd"),
                                                //----------inner try end-------------
                                                new ILInstruction(ByteCode.Nop, "Between inner try and inner finally"),
                                                //----------inner finally begin-------------
                                                new ILInstruction(ByteCode.Nop, "5. Inner finally start").AddLabel("FinBeg").Data(5),
                                                new ILInstruction(ByteCode.Nop, "6. Just before Endfinally").Data(6),
                                                new ILInstruction(ByteCode.Endfinally).AddLabel("FinEnd"),                            
                                                //----------inner finally end-------------

                                            new ILInstruction(ByteCode.Nop, "Just before outer try leave instruction").AddLabel("AfterInnerBlock"),
                                            new ILInstruction(ByteCode.Leave, new Label("TryLeaveDest")).AddLabel("OuterTryEnd"),                               
                                            //----------outer try end-------------
                                            new ILInstruction(ByteCode.Nop, "Between try and catch"),
                                            //----------outer catch begin-------------
                                            new ILInstruction(ByteCode.Nop, "7. At the catch start").AddLabel("CatchBeg").Data(7),
                                            new ILInstruction(ByteCode.Pop),
                                            new ILInstruction(ByteCode.Nop, "8. Just before catch leave instruction").Data(8),
                                            new ILInstruction(ByteCode.Leave, new Label("CatchLeaveDest")).AddLabel("CatchEnd"),                            
                                            //----------outer catch end-------------
                                            
                                            new ILInstruction(ByteCode.Nop, "Instruction where try leave points").AddLabel("TryLeaveDest"),
                                            new ILInstruction(ByteCode.Nop, "9(end). Instruction where catch leave points").AddLabel("CatchLeaveDest").Data(9),
                                            new ILInstruction(ByteCode.Ret)
                                        });

            mainMethod.EHTable.AddCatchHandler("OuterTryBeg", "OuterTryEnd", TempTypeLocator.ExceptionDesc.Metadata, "CatchBeg", "CatchEnd");
            mainMethod.EHTable.AddFinallyHandler("InnerTryBeg", "InnerTryEnd", "FinBeg", "FinEnd");

            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(mainMethod);

            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.ObjectDesc);
            domainModel.LoadType(TempTypeLocator.ExceptionDesc);
            domainModel.LoadType(TempTypeLocator.InvalidOperationExceptionDesc);

            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert  
            Assert.IsTrue(test.Success());
        }


        [TestMethod]
        public void ThreeMethodsWithExThrowingAndFinalliesAndCatches()
        {
            //Arrange
            ITest test = new CorrectOrderCheck();

            MethodDesc thirdMethod = new MethodDesc("ThirdMethod", 3)
                                        .AddCallback(test)
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Nop, "1. Just before outer tryfin try start").Data(5),               
                                                //----------outer tryfin try begin-------------
                                                new ILInstruction(ByteCode.Nop, "2. Outer tryfin try first instruction").AddLabel("OuterTryBeg").Data(6),

                                                    //----------mid try begin-------------
                                                    new ILInstruction(ByteCode.Nop, "3. At the mid try-catch try first instruction").AddLabel("MidTryBeg").Data(7),

                                                        //----------inner try begin-------------
                                                        new ILInstruction(ByteCode.Nop, "4. At the inner try first instruction").AddLabel("InnerTryBeg").Data(8),
                                                        new ILInstruction(ByteCode.Nop, "5. Just before throw").Data(9),
                                                        new ILInstruction(ByteCode.Newobj, TempTypeLocator.ExceptionDesc.Methods[0].Metadata /*ctor*/),
                                                        new ILInstruction(ByteCode.Throw),
                                                        new ILInstruction(ByteCode.Nop, "Just after throw"),
                                                        new ILInstruction(ByteCode.Leave, new Label("InnerLeaveTarget")).AddLabel("InnerTryEnd"),
                                                        //----------inner try end-------------
                                                        new ILInstruction(ByteCode.Nop, "Between inner try and inner finally"),
                                                        //----------inner finally begin-------------
                                                        new ILInstruction(ByteCode.Nop, "6. Inner finally start").AddLabel("InnerFinBeg").Data(10),
                                                        new ILInstruction(ByteCode.Nop, "7. Just before inner Endfinally").Data(11),
                                                        new ILInstruction(ByteCode.Endfinally).AddLabel("InnerFinEnd"),                            
                                                        //----------inner finally end-------------

                                                    new ILInstruction(ByteCode.Nop).AddLabel("InnerLeaveTarget"),
                                                    new ILInstruction(ByteCode.Leave, new Label("AfterMidBlock")).AddLabel("MidTryEnd"),  
                                                    //----------mid try end-------------

                                                    //----------mid catch begin-------------
                                                    new ILInstruction(ByteCode.Nop, "At the mid catch start").AddLabel("MidCatchBeg"),
                                                    new ILInstruction(ByteCode.Nop, "Just before mid catch leave instruction"),
                                                    new ILInstruction(ByteCode.Leave, new Label("AfterMidBlock")).AddLabel("MidCatchEnd"),   
                                                    //----------mid catch end-------------

                                                new ILInstruction(ByteCode.Nop, "Just before outer try leave instruction").AddLabel("AfterMidBlock"),
                                                new ILInstruction(ByteCode.Leave, new Label("OuterTryLeaveDest")).AddLabel("OuterTryEnd"), 
                                                //----------outer try end-------------
                                     
                                                //----------outer finally begin-------------
                                                new ILInstruction(ByteCode.Nop, "8. At the outer finally start").AddLabel("OuterFinBeg").Data(12),
                                                new ILInstruction(ByteCode.Nop, "9. Just before Endfinally").Data(13),
                                                new ILInstruction(ByteCode.Endfinally).AddLabel("OuterFinEnd"),                            
                                                //----------outer finally end-------------
                                       
                                            new ILInstruction(ByteCode.Ret).AddLabel("OuterTryLeaveDest")
                                        });

            thirdMethod.EHTable.AddFinallyHandler("InnerTryBeg", "InnerTryEnd", "InnerFinBeg", "InnerFinEnd");
            thirdMethod.EHTable.AddCatchHandler("MidTryBeg", "MidTryEnd", TempTypeLocator.InvalidOperationExceptionDesc.Metadata, "MidCatchBeg", "MidCatchEnd");
            thirdMethod.EHTable.AddFinallyHandler("OuterTryBeg", "OuterTryEnd", "OuterFinBeg", "OuterFinEnd");



            MethodDesc secondMethod = new MethodDesc("SecondMethod", 3)
                                     .AddCallback(test)
                                     .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                     .AddInstructions(new ILInstruction[]
                                     {              
                                                //----------outer try begin-------------
                                                new ILInstruction(ByteCode.Nop, "Outer try first instruction").AddLabel("OuterTryBeg").Data(3),

                                                    //----------inner try begin-------------
                                                    new ILInstruction(ByteCode.Nop, "At the inner try first instruction").AddLabel("InnerTryBeg").Data(4),

                                                    new ILInstruction(ByteCode.Call, thirdMethod.Metadata),

                                                    new ILInstruction(ByteCode.Leave, new Label("AfterMidBlock")).AddLabel("InnerTryEnd"),
                                                    //----------inner try end-------------
                                              
                                                    //----------inner finally begin-------------
                                                    new ILInstruction(ByteCode.Nop, "Inner finally start").AddLabel("InnerFinBeg").Data(14),
                                                    new ILInstruction(ByteCode.Nop, "Just before inner Endfinally").Data(15),
                                                    new ILInstruction(ByteCode.Endfinally).AddLabel("InnerFinEnd"),                            
                                                    //----------inner finally end-------------
                                                        
                                                new ILInstruction(ByteCode.Nop, "Just before outer try leave instruction").AddLabel("AfterMidBlock"),
                                                new ILInstruction(ByteCode.Leave, new Label("OuterTryLeaveDest")).AddLabel("OuterTryEnd"), 
                                                //----------outer try end-------------
                                     
                                                //----------outer finally begin-------------
                                                new ILInstruction(ByteCode.Nop, "At the outer finally start").AddLabel("OuterFinBeg").Data(16),
                                                new ILInstruction(ByteCode.Nop, "Just before Endfinally").Data(17),
                                                new ILInstruction(ByteCode.Endfinally).AddLabel("OuterFinEnd"),                            
                                                //----------outer finally end-------------
                                       
                                            new ILInstruction(ByteCode.Ret).AddLabel("OuterTryLeaveDest")
                                     });

            secondMethod.EHTable.AddFinallyHandler("InnerTryBeg", "InnerTryEnd", "InnerFinBeg", "InnerFinEnd");
            secondMethod.EHTable.AddFinallyHandler("OuterTryBeg", "OuterTryEnd", "OuterFinBeg", "OuterFinEnd");


            MethodDesc firstMethod = new MethodDesc("FirstMethod", 3)
                                        .EntryPoint()
                                        .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Global)
                                        .AddCallback(test)
                                        .AddLocals(new LocalVarDescription[] {
                                            new LocalVarDescription()
                                            {
                                                 SType = ESSlotType.HORef,
                                                 TypeToken = TempTypeLocator.ExceptionDesc.Metadata
                                            }
                                        })
                                        .AddInstructions(new ILInstruction[]
                                        {              
                                                            //----------outer try begin-------------
                                                            new ILInstruction(ByteCode.Nop, "Outer try first instruction").AddLabel("OuterTryBeg").Data(1),

                                                                //----------inner try begin-------------
                                                                new ILInstruction(ByteCode.Nop, "At the inner try first instruction").AddLabel("InnerTryBeg").Data(2),

                                                                new ILInstruction(ByteCode.Call, secondMethod.Metadata),

                                                                new ILInstruction(ByteCode.Leave, new Label("AfterMidBlock")).AddLabel("InnerTryEnd"),
                                                                //----------inner try end---------------
                                              
                                                                //----------inner finally begin-----------
                                                                new ILInstruction(ByteCode.Nop, "Inner finally start").AddLabel("InnerFinBeg").Data(18),
                                                                new ILInstruction(ByteCode.Nop, "Just before inner Endfinally").Data(19),
                                                                new ILInstruction(ByteCode.Endfinally).AddLabel("InnerFinEnd"),                            
                                                                //----------inner finally end-------------
                                                        
                                                            new ILInstruction(ByteCode.Nop, "Just before outer try leave instruction").AddLabel("AfterMidBlock"),
                                                            new ILInstruction(ByteCode.Leave, new Label("OuterTryLeaveDest")).AddLabel("OuterTryEnd"), 
                                                            //----------outer try end-------------
                                     
                                                            //----------outer catch begin-------------
                                                            new ILInstruction(ByteCode.Nop, "At the outer catch start").AddLabel("OuterCatchBeg").Data(20),
                                                            new ILInstruction(ByteCode.Stloc, 0),
                                                            new ILInstruction(ByteCode.Nop, "Just before outer catch leave").Data(21),
                                                            new ILInstruction(ByteCode.Leave, new Label("OuterTryLeaveDest") ).AddLabel("OuterCatchEnd"),                            
                                                            //----------outer catch end-------------
                                       
                                                        new ILInstruction(ByteCode.Ret).AddLabel("OuterTryLeaveDest").Data(22)
                                        });

            firstMethod.EHTable.AddFinallyHandler("InnerTryBeg", "InnerTryEnd", "InnerFinBeg", "InnerFinEnd");
            firstMethod.EHTable.AddCatchHandler("OuterTryBeg", "OuterTryEnd", TempTypeLocator.ExceptionDesc.Metadata, "OuterCatchBeg", "OuterCatchEnd");




            CompiledModel compiledModel = new CompiledModel()
                                        .AddMethod(firstMethod)
                                        .AddMethod(secondMethod)
                                        .AddMethod(thirdMethod);


            DomainModel domainModel = new DomainModel(new GCHeap.Factory(), new TypesHeap.Factory());
            domainModel.LoadType(TempTypeLocator.ObjectDesc);
            domainModel.LoadType(TempTypeLocator.ExceptionDesc);
            domainModel.LoadType(TempTypeLocator.InvalidOperationExceptionDesc);

            var executor = domainModel.GetExecutor(compiledModel, new ILOperationSet());

            //Act
            executor.Start();

            //Assert  
            Assert.IsTrue(test.Success());
        }
    }
}
