using System;
using System.Linq;
using System.Reflection; 
using System.Runtime.CompilerServices;
using System.Text;
using ILTool.Kernel;
using ILTool.Kernel.ExceptionHandling;
using ILTool.Kernel.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Loader.Tests
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            MethodInfo method = typeof(Int32).GetMethods()[1];
            MethodBody mbody = method.GetMethodBody();
            byte[] ilBytes = mbody.GetILAsByteArray();
            MethodLoader loader = new MethodLoader(method);

            //Act
            var rawInstructions = loader.GetRawInstructionsFromBytes(ilBytes);
            var ilInstructions = loader.ConvertRawInstructions(rawInstructions);

            //Assert
            Assert.AreEqual(14, ilInstructions.Count);
            Assert.AreEqual(ByteCode.Ldarg_0, ilInstructions[0].ILOperation);
            Assert.AreEqual(ByteCode.Ldind_I4, ilInstructions[1].ILOperation);
            Assert.AreEqual(ByteCode.Ldarg_1, ilInstructions[2].ILOperation);

            Assert.AreEqual(ByteCode.Bge_S, ilInstructions[3].ILOperation);
            Assert.AreEqual(6, ilInstructions[3].Operand);

            Assert.AreEqual(ByteCode.Ldc_I4_M1, ilInstructions[4].ILOperation);
            Assert.AreEqual(ByteCode.Ret, ilInstructions[5].ILOperation);
            Assert.AreEqual(ByteCode.Ldarg_0, ilInstructions[6].ILOperation);
            Assert.AreEqual(ByteCode.Ldind_I4, ilInstructions[7].ILOperation);
            Assert.AreEqual(ByteCode.Ldarg_1, ilInstructions[8].ILOperation);

            Assert.AreEqual(ByteCode.Ble_S, ilInstructions[9].ILOperation);
            Assert.AreEqual(12, ilInstructions[9].Operand);

            Assert.AreEqual(ByteCode.Ldc_I4_1, ilInstructions[10].ILOperation);
            Assert.AreEqual(ByteCode.Ret, ilInstructions[11].ILOperation);
            Assert.AreEqual(ByteCode.Ldc_I4_0, ilInstructions[12].ILOperation);
            Assert.AreEqual(ByteCode.Ret, ilInstructions[13].ILOperation);
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Arrange

            Action method = () =>
            {
                Int32 a = 5;
                try
                {
                    a = 6;
                }
                finally
                {
                    a = 7;
                }
            };

            //Act     
            MethodLoader loader = new MethodLoader(method.GetMethodInfo());
            IEHTable ehTable = loader.EHTable;

            //Assert
            var handlers = ehTable.GetAllHandlers();
            Assert.AreEqual(14, loader.ILInstructions.Count);
            Assert.AreEqual(1, handlers.Count);
            Assert.AreEqual(3, handlers[0].TryOffset);
            Assert.AreEqual(4, handlers[0].TryLength);
            Assert.AreEqual(8, handlers[0].HandlerOffset);
            Assert.AreEqual(4, handlers[0].HandlerLength);
            Assert.AreEqual(EHHandlerType.Finally, handlers[0].HandlerType);
            Assert.AreEqual(null, handlers[0].ExceptionToken);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            Action method = () =>
            {
                Int32 a = 5;
                try
                {
                    a = 6;
                    try
                    {
                        a = 9;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception in try");
                    }
                }
                finally
                {
                    try
                    {
                        a = 7;
                    }
                    catch (Exception ex)
                    {
                        a = 11;
                        Console.WriteLine("Exception in finally");
                    }
                }
            };

            //Act     
            MethodLoader loader = new MethodLoader(method.GetMethodInfo());
            IEHTable ehTable = loader.EHTable;

            //Assert
            Assert.AreEqual(38, loader.ILInstructions.Count);
            var handlers = ehTable.GetAllHandlers();
            Assert.AreEqual(3, handlers.Count);

            Assert.AreEqual(6, handlers[0].TryOffset);
            Assert.AreEqual(4, handlers[0].TryLength);
            Assert.AreEqual(11, handlers[0].HandlerOffset);
            Assert.AreEqual(6, handlers[0].HandlerLength);
            Assert.AreEqual(EHHandlerType.Catch, handlers[0].HandlerType);
            Assert.IsTrue(handlers[0].ExceptionToken.Equals(new ClassToken(typeof(System.Exception))));


            Assert.AreEqual(21, handlers[1].TryOffset);
            Assert.AreEqual(4, handlers[1].TryLength);
            Assert.AreEqual(26, handlers[1].HandlerOffset);
            Assert.AreEqual(8, handlers[1].HandlerLength);
            Assert.AreEqual(EHHandlerType.Catch, handlers[1].HandlerType);
            Assert.IsTrue(handlers[1].ExceptionToken.Equals(new ClassToken(typeof(System.Exception))));


            Assert.AreEqual(3, handlers[2].TryOffset);
            Assert.AreEqual(16, handlers[2].TryLength);
            Assert.AreEqual(20, handlers[2].HandlerOffset);
            Assert.AreEqual(16, handlers[2].HandlerLength);
            Assert.AreEqual(EHHandlerType.Finally, handlers[2].HandlerType);
            Assert.AreEqual(null, handlers[2].ExceptionToken);
        }


        [TestMethod]
        public void TestMethod4()
        {
            //Arrange 
            Action method = () =>
            {
                Int32 a = 5;
                string b = "qwerty";
                try
                {
                    a = 6;
                }
                finally
                {
                    a = 7;
                }
            };

            //Act     
            MethodLoader loader = new MethodLoader(method.GetMethodInfo());

            var locals = loader.Locals;

            //Assert 
            Assert.AreEqual(2, locals.Length);

            Assert.AreEqual(false, locals[0].IsPinned);
            Assert.AreEqual(null, locals[0].Name);
            Assert.IsTrue(locals[0].TypeToken.Equals(new ClassToken(typeof(System.Int32))));

            Assert.AreEqual(false, locals[1].IsPinned);
            Assert.AreEqual(null, locals[1].Name);
            Assert.IsTrue(locals[1].TypeToken.Equals(new ClassToken(typeof(System.String))));

        }

        //[TestMethod]
        //public void TestMethod5()
        //{
        //    Int64 big = 24234234534545;
        //    //Arrange 
        //    Action<Int32, String, Object> method = (Int32 first, String second, Object third) =>
        //    {
        //        Int32 a = 5;
        //        string b = "qwerty";
        //        try
        //        {
        //            a = 6;
        //            Console.WriteLine("qwe");
        //            var w = new Label("");
        //        }
        //        finally
        //        {
        //            a = 7;
        //        }
        //    };

        //    //Act     
        //    MethodLoader loader = new MethodLoader(method.GetMethodInfo());

        //    var arguments = loader.Arguments;
        //    var int32MetadataToken = arguments[0].MetadataToken;
        //    Type t = typeof(Int32);



        //    var resType = method.GetMethodInfo().Module.ResolveSignature(int32MetadataToken);


        //    // var resType = method.GetMethodInfo().Module.ResolveType(int32MetadataToken);

        //    //Assert 
        //    Assert.AreEqual(3, arguments.Length);

        //    //Assert.AreEqual(false, locals[0].IsPinned);
        //    //Assert.AreEqual(null, locals[0].Name);
        //    //Assert.IsTrue(locals[0].TypeToken.Equals(new ClassToken("System.Int32")));

        //    //Assert.AreEqual(false, locals[1].IsPinned);
        //    //Assert.AreEqual(null, locals[1].Name);
        //    //Assert.IsTrue(locals[1].TypeToken.Equals(new ClassToken("System.String")));

        //}
    }

}