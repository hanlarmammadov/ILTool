using ILTool.Kernel.Descriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Tests.Helpers
{
    public static class StubMethods
    {
        public static MethodDesc TakesTwoIntegersAndReturnsTheSum()
        {
            MethodDesc calc = new MethodDesc()
            {
                IsEntryPoint = false,
                MaxStackSize = 2,
                ReturnType = TempTypeLocator.Int32Desc.Metadata,
                Arguments = new MethodArgDescription[]
                  {
                        new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                        new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                  },
                Instructions = new ILInstruction[]
                  {
                      new ILInstruction(ByteCode.Ldarg, 0),
                      new ILInstruction(ByteCode.Ldarg, 1),
                      new ILInstruction(ByteCode.Add),
                      new ILInstruction(ByteCode.Ret)
                  }
            };
            return calc;
        }

        public static MethodDesc TakesTwoIntegersAndReturnsTheMul()
        {
            MethodDesc calc = new MethodDesc()
            {
                IsEntryPoint = false,
                MaxStackSize = 2,
                ReturnType = TempTypeLocator.Int32Desc.Metadata,
                Arguments = new MethodArgDescription[]
                  {
                        new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                        new MethodArgDescription(){TypeToken = TempTypeLocator.Int32Desc.Metadata},
                  },
                Instructions = new ILInstruction[]
                  {
                      new ILInstruction(ByteCode.Ldarg, 0),
                      new ILInstruction(ByteCode.Ldarg, 1),
                      new ILInstruction(ByteCode.Mul),
                      new ILInstruction(ByteCode.Ret)
                  }
            };
            return calc;
        }
    }
}
