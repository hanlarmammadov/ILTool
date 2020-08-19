using ILTool.Kernel.Exceptions;
using ILTool.Kernel.Metadata;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    //ToNativeInt = 1,           //Conv_I = 0xd3,   -
    //ToUNativeInt = 2,          //Conv_U = 0xe0,   -
    //ToSByte = 3,               //Conv_I1 = 0x67,
    //ToByte = 4,                //Conv_U1 = 0xd2,
    //ToInt16 = 5,               //Conv_I2 = 0x68,
    //ToUInt16 = 6,              //Conv_U2 = 0xd1,
    //ToInt32 = 7,               //Conv_I4 = 0x69,
    //ToUInt32 = 8,              //Conv_U4 = 0x6d,
    //ToInt64 = 9,               //Conv_I8 = 0x6a, 
    //ToUInt64 = 10,             //Conv_U8 = 0x6e,
    //ToSingleUn = 11,           //Conv_R_Un = 0x76,  -
    //ToSingle = 12,             //Conv_R4 = 0x6b,
    //ToDouble = 13,             //Conv_R8 = 0x6c,

    //ToNativeIntOvf=14,         //Conv_Ovf_I = 0xd4,
    //ToSByteOvf=16,             //Conv_Ovf_I1 = 0xb3,
    //ToInt16Ovf=18,             //Conv_Ovf_I2 = 0xb5,
    //ToInt32Ovf=20,             //Conv_Ovf_I4 = 0xb7,
    //ToInt64Ovf=22,             //Conv_Ovf_I8 = 0xb9,
    //ToUNativeIntOvf=24,        //Conv_Ovf_U = 0xd5,
    //ToByteOvf=26,              //Conv_Ovf_U1 = 0xb4,
    //ToUInt16Ovf=28,            //Conv_Ovf_U2 = 0xb6,
    //ToUInt32Ovf=30,            //Conv_Ovf_U4 = 0xb8,
    //ToUInt64Ovf=32,            //Conv_Ovf_U8 = 0xba,
    
    //ToNativeIntOvfUn=15,       //Conv_Ovf_I_Un = 0x8a, 
    //ToSByteOvfUn=17,           //Conv_Ovf_I1_Un = 0x82,
    //ToInt16OvfUn=19,           //Conv_Ovf_I2_Un = 0x83,
    //ToInt32OvfUn=21,           //Conv_Ovf_I4_Un = 0x84,
    //ToInt64OvfUn=23,           //Conv_Ovf_I8_Un = 0x85,
    //ToUNativeIntOvfUn=25,      //Conv_Ovf_U_Un = 0x8b,
    //ToByteOvfUn=27,            //Conv_Ovf_U1_Un = 0x86,
    //ToUInt16OvfUn=29,          //Conv_Ovf_U2_Un = 0x87,
    //ToUInt32OvfUn=31,          //Conv_Ovf_U4_Un = 0x88,
    //ToUInt64OvfUn=33,          //Conv_Ovf_U8_Un = 0x89,


    public class Conv_I4Engine : PrimitiveConversionEngine, IILOperationEngine
    {
        public void Execute(MethodContext context, MethodState state, object operand = null)
        {
            if (context.EvalStack.Count < 1)
                throw new InvalidStackSizeException("Evaluation stack should contain at least one element");

            ESSlot initSlot = context.EvalStack.Pop();

            if (!initSlot.TypeToken.IsPrimitive)
                throw new OperandsNotSupportedByOperationException(ByteCode.Conv_I4, initSlot);

            ESSlot resultSlot = Convert(initSlot.Val, initSlot.TypeToken.PrimitiveType, PrimitiveType.Int32);
             
            context.EvalStack.Push(resultSlot);
        }
    }
}
