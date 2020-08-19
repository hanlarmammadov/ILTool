using ILTool.Kernel.Metadata;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.BaseClasses
{
    public class PrimitivesEngine : IPrimitiveOperations
    {
        protected static UnaryOperation[,] _unaryOperations;
        protected static BinaryOperation[,] _binaryOperations;
        protected static UnaryOperation[,] _convertOperations;
        protected static PrimitiveType[,] _commonPrimitiveTypesMap;
        protected static PrimitiveType[] _primaryTypesMap;

        protected static ClassToken[] _primitiveTokens;
        protected const Int32 NUM_OF_BIN_OPERATIONS = 12;
        protected const Int32 NUM_OF_UN_OPERATIONS = 3;
        protected const Int32 NUM_OF_PRIMITIVE_TYPES = 14;

        static PrimitivesEngine()
        {
            _unaryOperations = new UnaryOperation[NUM_OF_PRIMITIVE_TYPES + 1, NUM_OF_UN_OPERATIONS];
            _binaryOperations = new BinaryOperation[NUM_OF_PRIMITIVE_TYPES + 1, NUM_OF_BIN_OPERATIONS];
            _commonPrimitiveTypesMap = new PrimitiveType[NUM_OF_PRIMITIVE_TYPES + 1, NUM_OF_PRIMITIVE_TYPES + 1];
            _primaryTypesMap = new PrimitiveType[NUM_OF_PRIMITIVE_TYPES + 1];
            _convertOperations = new UnaryOperation[NUM_OF_PRIMITIVE_TYPES + 1, NUM_OF_PRIMITIVE_TYPES + 1];
            _primitiveTokens = new ClassToken[NUM_OF_PRIMITIVE_TYPES + 1];


            //Int32
            AddInt32BinaryOperations();
            

            //Int64
            AddInt64BinaryOperations();

            //UInt64
            AddUInt64BinaryOperations();

            //UInt32
            AddUInt32BinaryOperations();

            //Single
            AddSingleBinaryOperations();

            //Double
            AddDoubleBinaryOperations();

            //Primitive types map
            FillCommonPrimitiveTypesMap();

            FillConvertOperations();

            FillPrimitiveTokens();
           
            FillPrimaryTypes();

            FillStoreAndStackRepresentations();

            FillUnaryOperations();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnaryOperation GetUnaryOperation(PrimitiveType primitive, UnaryPrimitiveOpType operation)
        {
            return _unaryOperations[(int)primitive, (int)operation];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BinaryOperation GetBinaryOperation(PrimitiveType primitive, BinaryPrimitiveOpType operation)
        {
            return _binaryOperations[(int)primitive, (int)operation];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPrimaryType(PrimitiveType primitive)
        {
            return _primaryTypesMap[(int)primitive] == primitive;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PrimitiveType GetCommonPrimaryType(PrimitiveType primitive1, PrimitiveType primitive2)
        {
            return _commonPrimitiveTypesMap[(int)primitive1, (int)primitive2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UnaryOperation GetConvertOperation(PrimitiveType fromPrimitive, PrimitiveType toPrimitive)
        {
            return _convertOperations[(int)fromPrimitive, (int)toPrimitive];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ClassToken GetTokenForPrimitive(PrimitiveType primitive)
        {
            return _primitiveTokens[(int)primitive];
        }
         
        #region Filling Operation arrays
         
        protected static void AddInt32BinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (Int32)((Int32)obj1 + (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (Int32)((Int32)obj1 - (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (Int32)((Int32)obj1 * (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 & (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 | (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 ^ (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 == (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 != (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 > (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 >= (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 < (Int32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int32, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int32)obj1 <= (Int32)obj2);
            };
        }
         
        protected static void AddInt64BinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (Int64)((Int64)obj1 + (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (Int64)((Int64)obj1 - (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (Int64)((Int64)obj1 * (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 & (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 | (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 ^ (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 == (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 != (Int64)obj2);
            };


            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 > (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 >= (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 < (Int64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Int64, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Int64)obj1 <= (Int64)obj2);
            };
        }
         
        protected static void AddUInt64BinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (UInt64)((UInt64)obj1 + (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (UInt64)((UInt64)obj1 - (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (UInt64)((UInt64)obj1 * (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 & (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 | (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 ^ (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 == (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 != (UInt64)obj2);
            };

            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 > (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 >= (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 < (UInt64)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt64, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt64)obj1 <= (UInt64)obj2);
            };
        }
         
        protected static void AddUInt32BinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (UInt32)((UInt32)obj1 + (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (UInt32)((UInt32)obj1 - (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (UInt32)((UInt32)obj1 * (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 & (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 | (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 ^ (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 == (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 != (UInt32)obj2);
            };


            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 > (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 >= (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 < (UInt32)obj2);
            };
            _binaryOperations[(int)PrimitiveType.UInt32, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((UInt32)obj1 <= (UInt32)obj2);
            };
        }
         
        protected static void AddSingleBinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (Single)((Single)obj1 + (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (Single)((Single)obj1 - (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (Single)((Single)obj1 * (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Single type does not support And operation");
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Single type does not support Or operation");
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Single type does not support Xor operation");
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 == (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 != (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 > (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 >= (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 < (Single)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Single, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Single)obj1 <= (Single)obj2);
            };
        }
         
        protected static void AddDoubleBinaryOperations()
        {
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
            {
                return (Double)((Double)obj1 + (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
            {
                return (Double)((Double)obj1 - (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
            {
                return (Double)((Double)obj1 * (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Double type does not support And operation");
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Double type does not support Or operation");
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
            {
                throw new NotSupportedException("Double type does not support Xor operation");
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 == (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 != (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 > (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 >= (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 < (Double)obj2);
            };
            _binaryOperations[(int)PrimitiveType.Double, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
            {
                return ((Double)obj1 <= (Double)obj2);
            };
        }
        
        protected static void FillCommonPrimitiveTypesMap()
        {
            //Boolean+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Boolean] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Byte] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.SByte] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int16] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt16] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int32] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt32] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Char] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Double] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Boolean, (int)PrimitiveType.Single] = PrimitiveType.NotPrimitive;

            //Byte+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt32] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Byte, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //SByte+
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.SByte, (int)PrimitiveType.Single] = PrimitiveType.Single;


            //Int16+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int16, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //UInt16+
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt32] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt16, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //Int32+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int32, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //UInt32+
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Byte] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.SByte] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int16] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt16] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt32] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Char] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt32, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //Int64+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Byte] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.SByte] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Int16] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt16] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Int32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt32] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Char] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Int64, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //UInt64+
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Byte] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.SByte] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int16] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt16] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int32] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt32] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Char] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UInt64, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //IntPtr+
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Byte] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.SByte] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int16] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt16] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int32] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt32] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Char] = PrimitiveType.IntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Double] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Single] = PrimitiveType.NotPrimitive;

            //UIntPtr+
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Byte] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.SByte] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int16] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt16] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int32] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt32] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt64] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Char] = PrimitiveType.UIntPtr;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Double] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Single] = PrimitiveType.NotPrimitive;

            ////Char+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.UInt32] = PrimitiveType.UInt32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Char] = PrimitiveType.Int32;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Char, (int)PrimitiveType.Single] = PrimitiveType.Single;

            //Double+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Byte] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.SByte] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Int16] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.UInt16] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Int32] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.UInt32] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Int64] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.UInt64] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Char] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Double, (int)PrimitiveType.Single] = PrimitiveType.Double;

            //Single+
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Boolean] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Byte] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.SByte] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Int16] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.UInt16] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Int32] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.UInt32] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Int64] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.UInt64] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.IntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.UIntPtr] = PrimitiveType.NotPrimitive;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Char] = PrimitiveType.Single;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Double] = PrimitiveType.Double;
            _commonPrimitiveTypesMap[(int)PrimitiveType.Single, (int)PrimitiveType.Single] = PrimitiveType.Single;
        }

        protected static void FillConvertOperations()
        {
            //Boolean+
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Boolean] = (Object obj) => { return (Boolean)(((Int32)obj) == 0 ? false : true); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Byte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.SByte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Int64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.UInt64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Double] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Boolean, (int)PrimitiveType.Single] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };

            //Byte+   //UInt16, Int16, UInt32, Int32, UInt64, Int64, Single, Double, Decimal | SByte
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Byte, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(Byte)(Int32)obj; };

            //SByte+    Int16, Int32, Int64, Single, Double, Decimal | Byte, UInt16, UInt32, UInt64
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot cast SByte to Boolean"); };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.SByte, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(SByte)(Int32)obj; };


            //Int16+    Int32, Int64, Single, Double, Decimal | Byte, SByte, UInt16
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.UInt64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int16, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(Int16)(Int32)obj; };

            //UInt16+   UInt32, Int32, UInt64, Int64, Single, Double, Decimal | Byte, SByte, Int16
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.UInt16, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(UInt16)(Int32)obj; };

            //Int32+    Int64, Double, Decimal, | Single | Byte, SByte, Int16, UInt16, UInt32
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Boolean] = (Object obj) => { return (Int32)obj; /*return (Boolean)(((Int32)obj) == 0 ? false : true);*/ };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Int32)obj; };
            _convertOperations[(int)PrimitiveType.Int32, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(Int32)obj; };

            //UInt32+   Int64, UInt64, Double, Decimal | Single | Byte, SByte, Int16, UInt16, Int32
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(UInt32)obj; };
            _convertOperations[(int)PrimitiveType.UInt32, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(UInt32)obj; };

            //Int64+    Decimal | Single, Double | Byte, SByte, Int16, UInt16, Int32, UInt32, UInt64
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Int64)obj; };
            _convertOperations[(int)PrimitiveType.Int64, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(Int64)obj; };

            //UInt64+   Decimal | Single, Double | Byte, SByte, Int16, UInt16, Int32, UInt32, Int64
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(UInt64)obj; };
            _convertOperations[(int)PrimitiveType.UInt64, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(UInt64)obj; };

            //IntPtr+
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Byte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.SByte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Int64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UInt64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Double] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.IntPtr, (int)PrimitiveType.Single] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };

            //UIntPtr+
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Byte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.SByte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt32] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Int64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UInt64] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Double] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.UIntPtr, (int)PrimitiveType.Single] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };

            //Char+     UInt16, UInt32, Int32, UInt64, Int64, Single, Double, Decimal
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Byte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.SByte] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Int16] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Char] = (Object obj) => { return (Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Char)obj; };
            _convertOperations[(int)PrimitiveType.Char, (int)PrimitiveType.Single] = (Object obj) => { return (Single)(Char)obj; };

            //Double+ | Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Double] = (Object obj) => { return (Double)obj; };
            _convertOperations[(int)PrimitiveType.Double, (int)PrimitiveType.Single] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };

            //Single+   Double | Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Boolean] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Byte] = (Object obj) => { return (Int32)(Byte)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.SByte] = (Object obj) => { return (Int32)(SByte)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Int16] = (Object obj) => { return (Int32)(Int16)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.UInt16] = (Object obj) => { return (Int32)(UInt16)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Int32] = (Object obj) => { return (Int32)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.UInt32] = (Object obj) => { return (UInt32)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Int64] = (Object obj) => { return (Int64)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.UInt64] = (Object obj) => { return (UInt64)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.IntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.UIntPtr] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Char] = (Object obj) => { throw new InvalidOperationException("Cannot convert"); };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Double] = (Object obj) => { return (Double)(Single)obj; };
            _convertOperations[(int)PrimitiveType.Single, (int)PrimitiveType.Single] = (Object obj) => { return (Single)obj; };
        }

        protected static void FillPrimitiveTokens()
        {
            _primitiveTokens[(int)PrimitiveType.Boolean] = new ClassToken("System.Boolean", true);
            _primitiveTokens[(int)PrimitiveType.Byte] = new ClassToken("System.Byte", true);
            _primitiveTokens[(int)PrimitiveType.SByte] = new ClassToken("System.SByte", true);
            _primitiveTokens[(int)PrimitiveType.Int16] = new ClassToken("System.Int16", true);
            _primitiveTokens[(int)PrimitiveType.UInt16] = new ClassToken("System.UInt16", true);
            _primitiveTokens[(int)PrimitiveType.Int32] = new ClassToken("System.Int32", true);
            _primitiveTokens[(int)PrimitiveType.UInt32] = new ClassToken("System.UInt32", true);
            _primitiveTokens[(int)PrimitiveType.Int64] = new ClassToken("System.Int64", true);
            _primitiveTokens[(int)PrimitiveType.UInt64] = new ClassToken("System.UInt64", true);
            _primitiveTokens[(int)PrimitiveType.IntPtr] = new ClassToken("System.IntPtr", true);
            _primitiveTokens[(int)PrimitiveType.UIntPtr] = new ClassToken("System.UIntPtr", true);
            _primitiveTokens[(int)PrimitiveType.Char] = new ClassToken("System.Char", true);
            _primitiveTokens[(int)PrimitiveType.Double] = new ClassToken("System.Double", true);
            _primitiveTokens[(int)PrimitiveType.Single] = new ClassToken("System.Single", true);
        }

        protected static void FillPrimaryTypes()
        {
            _primaryTypesMap[(int)PrimitiveType.Boolean] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.Byte] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.SByte] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.Int16] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.UInt16] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.Int32] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.UInt32] = PrimitiveType.UInt32;
            _primaryTypesMap[(int)PrimitiveType.Int64] = PrimitiveType.Int64;
            _primaryTypesMap[(int)PrimitiveType.UInt64] = PrimitiveType.UInt64;
            _primaryTypesMap[(int)PrimitiveType.IntPtr] = PrimitiveType.IntPtr;
            _primaryTypesMap[(int)PrimitiveType.UIntPtr] = PrimitiveType.UIntPtr;
            _primaryTypesMap[(int)PrimitiveType.Char] = PrimitiveType.Int32;
            _primaryTypesMap[(int)PrimitiveType.Double] = PrimitiveType.Double;
            _primaryTypesMap[(int)PrimitiveType.Single] = PrimitiveType.Single;
        }

        protected static void FillStoreAndStackRepresentations()
        {
            //GetStackRep
            _unaryOperations[(int)PrimitiveType.Boolean, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(((Boolean)obj) ? 1 : 0); };
            _unaryOperations[(int)PrimitiveType.Byte, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(Byte)obj; };
            _unaryOperations[(int)PrimitiveType.SByte, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(SByte)obj; };
            _unaryOperations[(int)PrimitiveType.Int16, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(Int16)obj; };
            _unaryOperations[(int)PrimitiveType.UInt16, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(UInt16)obj; };
            _unaryOperations[(int)PrimitiveType.Char, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)(Char)obj; };
            //_unaryOperations[(int)PrimitiveType.Int32, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int32)obj; };
            //_unaryOperations[(int)PrimitiveType.Int64, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Int64)obj; };
            //_unaryOperations[(int)PrimitiveType.UInt64, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (UInt64)obj; };
            //_unaryOperations[(int)PrimitiveType.UInt32, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (UInt32)obj; };
            //_unaryOperations[(int)PrimitiveType.Single, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Single)obj; };
            //_unaryOperations[(int)PrimitiveType.Double, (int)UnaryPrimitiveOpType.GetStackRep] = (Object obj) => { return (Double)obj; };


            //GetStoreRep
            _unaryOperations[(int)PrimitiveType.Boolean, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Boolean)((Int32)obj == 0) ? false : true; };
            _unaryOperations[(int)PrimitiveType.Byte, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Byte)(Int32)obj; };
            _unaryOperations[(int)PrimitiveType.SByte, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (SByte)(Int32)obj; };
            _unaryOperations[(int)PrimitiveType.Int16, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Int16)(Int32)obj; };
            _unaryOperations[(int)PrimitiveType.UInt16, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (UInt16)(Int32)obj; };
            _unaryOperations[(int)PrimitiveType.Char, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Char)(Int32)obj; };
            //_unaryOperations[(int)PrimitiveType.Int32, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Int32)obj; };
            //_unaryOperations[(int)PrimitiveType.Int64, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Int64)obj; };
            //_unaryOperations[(int)PrimitiveType.UInt64, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (UInt64)obj; };
            //_unaryOperations[(int)PrimitiveType.UInt32, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (UInt32)obj; };
            //_unaryOperations[(int)PrimitiveType.Single, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Single)obj; };
            //_unaryOperations[(int)PrimitiveType.Double, (int)UnaryPrimitiveOpType.GetStoreRep] = (Object obj) => { return (Double)obj; };
            
        }

        protected static void FillUnaryOperations()
        {
            //Boolean
            //_unaryOperations[(int)PrimitiveType.Boolean, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { return (!(Boolean)obj); };

            //Int32
            _unaryOperations[(int)PrimitiveType.Int32, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { return (~(Int32)obj); };

            //Int64
            _unaryOperations[(int)PrimitiveType.Int64, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { return (~(Int64)obj); };

            //UInt64
            _unaryOperations[(int)PrimitiveType.UInt64, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { return (~(UInt64)obj); };

            //UInt32
            _unaryOperations[(int)PrimitiveType.UInt32, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { return (~(UInt32)obj); };

            //Single
            _unaryOperations[(int)PrimitiveType.Single, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { throw new NotSupportedException("Single type does not support Not operation"); };

            //Double
            _unaryOperations[(int)PrimitiveType.Double, (int)UnaryPrimitiveOpType.Not] = (Object obj) => { throw new NotSupportedException("Double type does not support Not operation"); };

        }

        #endregion
    }
}



#region

//protected static void AddByteBinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        return (Byte)((Byte)obj1 + (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        return (Byte)((Byte)obj1 - (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        return (Byte)((Byte)obj1 * (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 & (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 | (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 ^ (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 == (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 != (Byte)obj2);
//    };

//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 > (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 >= (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 < (Byte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Byte, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Byte)obj1 <= (Byte)obj2);
//    };
//}


//protected static void AddSByteBinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        return (SByte)((SByte)obj1 + (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        return (SByte)((SByte)obj1 - (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        return (SByte)((SByte)obj1 * (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 & (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 | (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 ^ (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 == (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 != (SByte)obj2);
//    };


//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 > (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 >= (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 < (SByte)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.SByte, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((SByte)obj1 <= (SByte)obj2);
//    };
//}


//protected static void AddInt16BinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        return (Int16)((Int16)obj1 + (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        return (Int16)((Int16)obj1 - (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        return (Int16)((Int16)obj1 * (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 & (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 | (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 ^ (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 == (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 != (Int16)obj2);
//    };


//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 > (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 >= (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 < (Int16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Int16, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Int16)obj1 <= (Int16)obj2);
//    };
//}


//protected static void AddUInt16BinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        return (UInt16)((UInt16)obj1 + (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        return (UInt16)((UInt16)obj1 - (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        return (UInt16)((UInt16)obj1 * (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 & (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 | (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 ^ (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 == (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 != (UInt16)obj2);
//    };

//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 > (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 >= (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 < (UInt16)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.UInt16, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((UInt16)obj1 <= (UInt16)obj2);
//    };
//}


//protected static void AddCharBinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        return (Char)((Char)obj1 + (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        return (Char)((Char)obj1 - (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        return (Char)((Char)obj1 * (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 & (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 | (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 ^ (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 == (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 != (Char)obj2);
//    }; 
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 > (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 >= (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 < (Char)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Char, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Char)obj1 <= (Char)obj2);
//    };
//}



//protected static void AddBooleanBinaryOperations()
//{
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Add] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support Add operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Sub] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support Sub operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Mul] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support Mul operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.And] = (Object obj1, Object obj2) =>
//    {
//        return ((Boolean)obj1 && (Boolean)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Or] = (Object obj1, Object obj2) =>
//    {
//        return ((Boolean)obj1 || (Boolean)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Xor] = (Object obj1, Object obj2) =>
//    {
//        return ((Boolean)obj1 ^ (Boolean)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Equal] = (Object obj1, Object obj2) =>
//    {
//        return ((Boolean)obj1 == (Boolean)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.NotEqual] = (Object obj1, Object obj2) =>
//    {
//        return ((Boolean)obj1 != (Boolean)obj2);
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Greater] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support Greater operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.GreaterOrEqual] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support GreaterOrEqual operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.Less] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support Less operation");
//    };
//    _binaryOperations[(int)PrimitiveType.Boolean, (int)BinaryPrimitiveOpType.LessOrEqual] = (Object obj1, Object obj2) =>
//    {
//        throw new NotSupportedException("Boolean type does not support LessOrEqual operation");
//    };
//}

#endregion