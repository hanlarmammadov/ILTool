using System;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILTool.Kernel.Tests
{
    [TestClass]
    public class Conv_i4EngineTests
    {
        [TestMethod]
        public void TestMethod1()
        { 
            Double d = 32D;
            Single s = 44F;
            Int64 i64 = 22L;
            Int32 i32 = 66;

            UInt32 ui32 = 342U;
            UInt64 ui64 = 342UL;

            //OpCodes.Conv_I8;
            //OpCodes.Conv_R8
        }
    }
}
