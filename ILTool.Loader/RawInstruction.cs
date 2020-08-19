using ILTool.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Loader
{
    public class RawInstruction
    {
        public ByteCode ByteCode;
        public OpCode OpCode;
        public Object Operand;
        public UInt32 Offset;
        public UInt32 Length;
         

        private static OpCode GetOpCode(ByteCode byteCode)
        { 
            var name = Enum.GetName(typeof(ByteCode), byteCode);
            Type opCodesType = typeof(OpCodes);
            FieldInfo myFieldInfo = opCodesType.GetField(name);
            Object opcode = myFieldInfo.GetValue(opCodesType);
            return (OpCode)opcode;
        }

        public RawInstruction(byte[] ilBytes, ref uint currInd)
        { 
            UInt16 opCode;
            uint byteCodeOffset = currInd;
            if (ilBytes[currInd] == 0xFE)
                opCode = (UInt16)(ilBytes[currInd] | ilBytes[++currInd] << 8);
            else
                opCode = ilBytes[currInd];

            this.ByteCode = (ByteCode)opCode;
            this.OpCode = GetOpCode(this.ByteCode); 
            this.Offset = currInd;

            switch (this.OpCode.OperandType)
            {
                case OperandType.InlineNone:
                    {
                        this.Operand = null;
                        break;
                    }
                case OperandType.InlineBrTarget:
                    {
                        //The operand is an 8-bit integer branch target.  checked
                        {
                            this.Operand = (Int32)(ilBytes[++currInd] | ilBytes[++currInd] << 8 | ilBytes[++currInd] << 16 | ilBytes[++currInd] << 24);
                            //this.Operand = ((Int32)this.Operand); //+ (Int32)currInd + 1;
                        }
                        break;
                    }
                case OperandType.ShortInlineBrTarget:
                    {
                        //The operand is an 8-bit integer branch target.
                        checked
                        {
                            //this.Operand = (Int16)ilBytes[++currInd];
                            this.Operand = (SByte)ilBytes[++currInd];
                            //this.Operand = ((SByte)this.Operand); // + (Int32)currInd + 1;
                        }
                        var t = this.Operand.GetType(); 
                        break;
                    }
                case OperandType.InlineSig:
                case OperandType.InlineString:
                case OperandType.InlineTok:
                case OperandType.InlineField:
                case OperandType.InlineType:
                case OperandType.InlineMethod:
                case OperandType.InlineI:
                    {
                        //The operand is a 32-bit integer.
                        this.Operand = (Int32)(ilBytes[++currInd] | ilBytes[++currInd] << 8 | ilBytes[++currInd] << 16 | ilBytes[++currInd] << 24);
                        break;
                    }
                case OperandType.InlineI8:
                    {
                        //The operand is a 64-bit integer.
                        this.Operand = (Int64)(ilBytes[++currInd] | ilBytes[++currInd] << 8 | ilBytes[++currInd] << 16 | ilBytes[++currInd] << 24 | ilBytes[++currInd] << 32 | ilBytes[++currInd] << 40 | ilBytes[++currInd] << 48 | ilBytes[++currInd] << 56);
                        break;
                    }
                case OperandType.ShortInlineR:
                    {
                        //The operand is a 32-bit IEEE floating point number.
                        this.Operand = (Single)(ilBytes[++currInd] | ilBytes[++currInd] << 8 | ilBytes[++currInd] << 16 | ilBytes[++currInd] << 24);
                        break;
                    }
                case OperandType.InlineR:
                    {
                        //The operand is a 64-bit IEEE floating point number.
                        this.Operand = (Double)(ilBytes[++currInd] | ilBytes[++currInd] << 8 | ilBytes[++currInd] << 16 | ilBytes[++currInd] << 24 | ilBytes[++currInd] << 32 | ilBytes[++currInd] << 40 | ilBytes[++currInd] << 48 | ilBytes[++currInd] << 56);
                        break;
                    }
                case OperandType.InlineSwitch:
                    {
                        //The operand is the 32-bit integer argument to a switch thisruction.
                        throw new NotImplementedException("OperandType.InlineSwitch has not been implemented");
                    }
                case OperandType.InlineVar:
                    {
                        //The operand is 16-bit integer containing the ordinal of a local variable or an argument.
                        this.Operand = (UInt16)(ilBytes[++currInd] | ilBytes[++currInd] << 8);
                        break;
                    }
                case OperandType.ShortInlineI:
                    {
                        //The operand is an 8-bit integer.
                        this.Operand = (SByte)ilBytes[++currInd];
                        break;
                    }
                case OperandType.ShortInlineVar:
                    {
                        //The operand is an 8-bit integer containing the ordinal of a local variable or an argument.
                        this.Operand = (Byte)ilBytes[++currInd];
                        break;
                    }
            }
            this.Length = currInd - this.Offset + 1;
        }

    }
}
