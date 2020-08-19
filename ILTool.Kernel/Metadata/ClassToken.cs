using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Metadata
{
    public class ClassToken : ImmutableToken
    {
        public readonly String Name;
        public readonly Int32 TypeMetadataTokenRaw;
        public readonly bool IsValueType;
        public readonly bool IsPrimitive;
        public readonly PrimitiveType PrimitiveType;

        public ClassToken(String name, bool isPrimitive)
        {
            Name = name;
            SetHashCode(this.Name.GetHashCode());
            if (isPrimitive)
            {
                IsPrimitive = true;
                PrimitiveType = GetPrimitiveType(name);
            }
        }

        public ClassToken(Type type)
        {
            //Name = $"Assembly: {type.Assembly.FullName} Type: {type.FullName}";
            Name = $"{type.FullName}";
            TypeMetadataTokenRaw = type.MetadataToken; 
            SetHashCode(this.Name.GetHashCode());
            if (type.IsPrimitive)
            {
                IsPrimitive = true;
                PrimitiveType = GetPrimitiveType(type.FullName);
            }
        }

        protected PrimitiveType GetPrimitiveType(String typeName)
        {
            var result = PrimitiveType.NotPrimitive;
            switch (typeName)
            {
                case "System.Boolean":
                    result = PrimitiveType.Boolean;
                    break;
                case "System.Byte":
                    result = PrimitiveType.Byte;
                    break;
                case "System.SByte":
                    result = PrimitiveType.SByte;
                    break;
                case "System.Int16":
                    result = PrimitiveType.Int16;
                    break;
                case "System.UInt16":
                    result = PrimitiveType.UInt16;
                    break;
                case "System.Int32":
                    result = PrimitiveType.Int32;
                    break;
                case "System.UInt32":
                    result = PrimitiveType.UInt32;
                    break;
                case "System.Int64":
                    result = PrimitiveType.Int64;
                    break;
                case "System.UInt64":
                    result = PrimitiveType.UInt64;
                    break;
                case "System.IntPtr":
                    result = PrimitiveType.IntPtr;
                    break;
                case "System.UIntPtr":
                    result = PrimitiveType.UIntPtr;
                    break;
                case "System.Char":
                    result = PrimitiveType.Char;
                    break;
                case "System.Double":
                    result = PrimitiveType.Double;
                    break;
                case "System.Single":
                    result = PrimitiveType.Single;
                    break;
                default:
                    throw new InvalidOperationException("Invalid primitive type name");
            };
            return result;
        }
    }
}
