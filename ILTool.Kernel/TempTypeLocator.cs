using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    //Should be elimenated as soon as possible
    public static class TempTypeLocator
    {
        public static TypeDesc ObjectDesc { get; private set; }

        public static TypeDesc ValueTypeDesc { get; private set; }

        public static TypeDesc Int32Desc { get; private set; }

        public static TypeDesc BooleanDesc { get; private set; }

        public static TypeDesc ByteDesc { get; private set; }

        public static TypeDesc SByteDesc { get; private set; }

        public static TypeDesc Int64Desc { get; private set; }

        public static TypeDesc UInt64Desc { get; private set; }

        public static TypeDesc UInt32Desc { get; private set; }

        public static TypeDesc Int16Desc { get; private set; }

        public static TypeDesc UInt16Desc { get; private set; }




        public static TypeDesc ExceptionDesc { get; private set; }

        public static TypeDesc InvalidOperationExceptionDesc { get; private set; }

        static List<MethodDesc> ObjTypeMethods()
        {
            MethodDesc getType = new MethodDesc("Type GetType()", 1)
                                    .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                                      .AddInheritanceAttributeFlag(MethodInheritanceAttributes.NewSlot)
                                    .BelongsTo(ObjectDesc)
                                    .AddReturnType(new ClassToken("System.Int32", true))
                                    .AddInstructions(new ILInstruction[]
                                        {
                                            new ILInstruction(ByteCode.Ldc_I4, 42),
                                            new ILInstruction(ByteCode.Ret)
                                        });
            MethodDesc toString = new MethodDesc("String ToString()", 1)
                                  .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                                  .AddInheritanceAttributeFlag(MethodInheritanceAttributes.NewSlot)
                                  .BelongsTo(ObjectDesc)
                                  .AddReturnType(new ClassToken("System.Int32", true))
                                  .AddInstructions(new ILInstruction[]
                                      {
                                            new ILInstruction(ByteCode.Ldc_I4, 52),
                                            new ILInstruction(ByteCode.Ret)
                                      });

            MethodDesc bar = new MethodDesc("void Bar()", 1)
                             .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                             .AddInheritanceAttributeFlag(MethodInheritanceAttributes.NewSlot)
                             .BelongsTo(ObjectDesc)
                             .AddReturnType(new ClassToken("System.Int32", true))
                             .AddInstructions(new ILInstruction[]
                                 {
                                            new ILInstruction(ByteCode.Ldc_I4, 77),
                                            new ILInstruction(ByteCode.Ret)
                                 });

            MethodDesc foobar = new MethodDesc("void Foobar()", 1)
                             .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                             .AddInheritanceAttributeFlag(MethodInheritanceAttributes.NewSlot)
                             .BelongsTo(ObjectDesc)
                             .AddReturnType(new ClassToken("System.Int32", true))
                             .AddInstructions(new ILInstruction[]
                                 {
                                            new ILInstruction(ByteCode.Ldc_I4, 77),
                                            new ILInstruction(ByteCode.Ret)
                                 });

            return new List<MethodDesc>() { getType, toString, bar, foobar };
        }

        static List<MethodDesc> Int32TypeMethods()
        {
            MethodDesc toString = new MethodDesc("String ToString()", 1)
                                  .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                                  .BelongsTo(Int32Desc)
                                  .AddReturnType(new ClassToken("System.Int32", true))
                                  .AddInstructions(new ILInstruction[]
                                      {
                                            new ILInstruction(ByteCode.Ldc_I4, 152),
                                            new ILInstruction(ByteCode.Ret)
                                      });

            MethodDesc foo = new MethodDesc("Int32 Foo()", 1)
                              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.NewSlot)
                              .BelongsTo(Int32Desc)
                              .AddReturnType(new ClassToken("System.Int32", true))
                              .AddInstructions(new ILInstruction[]
                                  {
                                            new ILInstruction(ByteCode.Ldc_I4, 555),
                                            new ILInstruction(ByteCode.Ret)
                                  });

            MethodDesc bar = new MethodDesc("void Bar()", 1)
                              .BelongsTo(Int32Desc)
                              .AddReturnType(new ClassToken("System.Int32", true))
                              .AddInstructions(new ILInstruction[]
                                {
                                                    new ILInstruction(ByteCode.Ldc_I4, 777),
                                                    new ILInstruction(ByteCode.Ret)
                                });

            MethodDesc foobar = new MethodDesc("void Foobar()", 1)
                              .AddInheritanceAttributeFlag(MethodInheritanceAttributes.Virtual)
                              .BelongsTo(Int32Desc)
                              .AddReturnType(new ClassToken("System.Int32", true))
                              //.Override()
                              .AddInstructions(new ILInstruction[]
                                  {
                                                    new ILInstruction(ByteCode.Ldc_I4, 77),
                                                    new ILInstruction(ByteCode.Ret)
                                  });

            return new List<MethodDesc>() { toString, foo, bar, foobar };
        }

        static List<MethodDesc> ExceptionTypeMethods()
        {
            MethodDesc ctor = new MethodDesc(".ctor()", 1)
                       .AddPropertiesFlag(MethodOtherProps.SpecialName)
                       .BelongsTo(ExceptionDesc)
                       .AddReturnType(new ClassToken("System.Int32", true))
                       .AddArguments(new MethodArgDescription[]
                           {
                                new MethodArgDescription(){TypeToken = ExceptionDesc.Metadata, SType= ESSlotType.HORef /*this*/},
                           })
                       .AddInstructions(new ILInstruction[]
                           {
                                new ILInstruction(ByteCode.Ldarg, 0),
                                new ILInstruction(ByteCode.Ret)
                           });

            return new List<MethodDesc>() { ctor };
        }

        static List<MethodDesc> InvalidOperationExceptionTypeMethods()
        {
            MethodDesc ctor = new MethodDesc(".ctor()", 1)
                       .AddPropertiesFlag(MethodOtherProps.SpecialName)
                       .BelongsTo(InvalidOperationExceptionDesc)
                       .AddReturnType(new ClassToken("System.Int32", true))
                       .AddArguments(new MethodArgDescription[]
                           {
                                new MethodArgDescription(){TypeToken = InvalidOperationExceptionDesc.Metadata, SType= ESSlotType.HORef /*this*/},
                           })
                       .AddInstructions(new ILInstruction[]
                           {
                                new ILInstruction(ByteCode.Ldarg, 0),
                                new ILInstruction(ByteCode.Ret)
                           });

            return new List<MethodDesc>() { ctor };
        }

        static TempTypeLocator()
        {
            ObjectDesc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Object)).Name,
                IsPrimitive = false,
                IsValueType = false,
                BaseClass = null
            };
            ObjectDesc.Methods = ObjTypeMethods();

            ValueTypeDesc = new TypeDesc()
            {
                Name = new ClassToken(typeof(ValueType)).Name,    /*"System.ValueType"*/
                IsPrimitive = false,
                IsValueType = false,
                BaseClass = ObjectDesc
            };

            Int32Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Int32)).Name, // "System.Int32",
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (Int32)((Int32)obj1 + (Int32)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (Int32)((Int32)obj1 - (Int32)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (Int32)((Int32)obj1 * (Int32)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((Int32)obj1 == (Int32)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((Int32)obj1 != (Int32)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((Int32)obj1 & (Int32)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((Int32)obj1 | (Int32)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((Int32)obj1 ^ (Int32)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(Int32)obj);
                //}
            };

            Int32Desc.Methods = Int32TypeMethods();

            ExceptionDesc = new TypeDesc()
            {
                Name = "System.Exception",
                BaseClass = ObjectDesc,

            };
            ExceptionDesc.Methods = ExceptionTypeMethods();

            InvalidOperationExceptionDesc = new TypeDesc()
            {
                Name = "System.InvalidOperationException",
                BaseClass = ExceptionDesc,
            };
            InvalidOperationExceptionDesc.Methods = InvalidOperationExceptionTypeMethods();

            BooleanDesc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Boolean)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    throw new NotSupportedException("Boolean type does not have Add operation");
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    throw new NotSupportedException("Boolean type does not have Add operation");
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    throw new NotSupportedException("Boolean type does not have Add operation");
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((Boolean)obj1 == (Boolean)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((Boolean)obj1 != (Boolean)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((Boolean)obj1 && (Boolean)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((Boolean)obj1 || (Boolean)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((Boolean)obj1 ^ (Boolean)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (!(Boolean)obj);
                //}
            };

            ByteDesc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Byte)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (Byte)((Byte)obj1 + (Byte)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (Byte)((Byte)obj1 - (Byte)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (Byte)((Byte)obj1 * (Byte)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((Byte)obj1 == (Byte)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((Byte)obj1 != (Byte)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((Byte)obj1 & (Byte)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((Byte)obj1 | (Byte)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((Byte)obj1 ^ (Byte)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(Byte)obj);
                //}
            };

            SByteDesc = new TypeDesc()
            {
                Name = new ClassToken(typeof(SByte)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (SByte)((SByte)obj1 + (SByte)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (SByte)((SByte)obj1 - (SByte)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (SByte)((SByte)obj1 * (SByte)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((SByte)obj1 == (SByte)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((SByte)obj1 != (SByte)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((SByte)obj1 & (SByte)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((SByte)obj1 | (SByte)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((SByte)obj1 ^ (SByte)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(SByte)obj);
                //}
            };

            Int64Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Int64)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (Int64)((Int64)obj1 + (Int64)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (Int64)((Int64)obj1 - (Int64)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (Int64)((Int64)obj1 * (Int64)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((Int64)obj1 == (Int64)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((Int64)obj1 != (Int64)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((Int64)obj1 & (Int64)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((Int64)obj1 | (Int64)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((Int64)obj1 ^ (Int64)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(Int64)obj);
                //}
            };

            UInt64Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(UInt64)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (UInt64)((UInt64)obj1 + (UInt64)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (UInt64)((UInt64)obj1 - (UInt64)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (UInt64)((UInt64)obj1 * (UInt64)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt64)obj1 == (UInt64)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt64)obj1 != (UInt64)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt64)obj1 & (UInt64)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt64)obj1 | (UInt64)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt64)obj1 ^ (UInt64)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(UInt64)obj);
                //}
            };

            UInt32Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(UInt32)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (UInt32)((UInt32)obj1 + (UInt32)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (UInt32)((UInt32)obj1 - (UInt32)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (UInt32)((UInt32)obj1 * (UInt32)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt32)obj1 == (UInt32)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt32)obj1 != (UInt32)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt32)obj1 & (UInt32)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt32)obj1 | (UInt32)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt32)obj1 ^ (UInt32)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(UInt32)obj);
                //}
            };

            Int16Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(Int16)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (Int16)((Int16)obj1 + (Int16)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (Int16)((Int16)obj1 - (Int16)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (Int16)((Int16)obj1 * (Int16)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((Int16)obj1 == (Int16)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((Int16)obj1 != (Int16)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((Int16)obj1 & (Int16)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((Int16)obj1 | (Int16)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((Int16)obj1 ^ (Int16)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(Int16)obj);
                //}
            };

            UInt16Desc = new TypeDesc()
            {
                Name = new ClassToken(typeof(UInt16)).Name,
                IsPrimitive = true,
                IsValueType = true,
                BaseClass = ValueTypeDesc,
                //Add = (Object obj1, Object obj2) =>
                //{
                //    return (UInt16)((UInt16)obj1 + (UInt16)obj2);
                //},
                //Sub = (Object obj1, Object obj2) =>
                //{
                //    return (UInt16)((UInt16)obj1 - (UInt16)obj2);
                //},
                //Mul = (Object obj1, Object obj2) =>
                //{
                //    return (UInt16)((UInt16)obj1 * (UInt16)obj2);
                //},
                //Equal = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt16)obj1 == (UInt16)obj2);
                //},
                //NotEqual = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt16)obj1 != (UInt16)obj2);
                //},
                //And = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt16)obj1 & (UInt16)obj2);
                //},
                //Or = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt16)obj1 | (UInt16)obj2);
                //},
                //Xor = (Object obj1, Object obj2) =>
                //{
                //    return ((UInt16)obj1 ^ (UInt16)obj2);
                //},
                //Not = (Object obj) =>
                //{
                //    return (~(UInt16)obj);
                //}
            };
        }
    }
}
