using ILTool.Kernel;
using ILTool.Kernel.Descriptions;
using ILTool.Kernel.ExceptionHandling;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Loader
{
    public class MethodLoader
    {
        public MethodInfo MethodInfo { get; private set; }
        public List<RawInstruction> RawInstructions { get; private set; }
        public List<ILInstruction> ILInstructions { get; private set; }
        public IEHTable EHTable { get; private set; }
        public LocalVarDescription[] Locals { get; private set; }
        public Int32 MaxStackSize { get; private set; }
        public String MethodName { get; private set; }
        public MethodArgDescription[] Arguments { get; private set; }
        public MethodInheritanceAttributes InheritanceAttributes { get; private set; }
        public MethodOtherProps Properties { get; private set; }
        public MemberAccessibility MemberAccessibility { get; private set; }
        public ClassToken ReturnType { get; private set; }

        public MethodLoader(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            var mbody = MethodInfo.GetMethodBody();
              
            RawInstructions = GetRawInstructionsFromBytes(mbody.GetILAsByteArray());
            ILInstructions = ConvertRawInstructions(RawInstructions);
            EHTable = CreateEHTable(mbody.ExceptionHandlingClauses);
            Locals = ExtractLocals();
            MaxStackSize = MethodInfo.GetMethodBody().MaxStackSize;
            MethodName = MethodInfo.Name;
            ReturnType = (MethodInfo.ReturnType != null) ? new ClassToken(MethodInfo.ReturnType) : null;
            Arguments = ExtractArguments();
            InheritanceAttributes = ExtractInheritanceAttrs();
            Properties = ExtractProperties();
            MemberAccessibility = ExtractMemberAccessibility();
        }

        public MethodDesc ConstructMethodDesc()
        {
            MethodDesc methodDesc = new MethodDesc(MethodName, MaxStackSize)
                        .AddInstructions(ILInstructions.ToArray())
                        .AddEHTable(EHTable)
                        .AddLocals(Locals)
                        .AddArguments(Arguments)
                        .AddInheritanceAttributes(InheritanceAttributes)
                        .AddProperties(Properties)
                        .AddMemberAccessibility(MemberAccessibility)
                        .AddReturnType(ReturnType);
            return methodDesc; 
        }
        
        public List<RawInstruction> GetRawInstructionsFromBytes(byte[] ilBytes)
        {
            List<RawInstruction> instructions = new List<RawInstruction>();
            uint currInd = 0;
            while (currInd < ilBytes.Length)
            {
                instructions.Add(new RawInstruction(ilBytes, ref currInd));
                currInd++;
            }
            return instructions;
        }

        private Int32 GetInstructionIndexByOffset(List<RawInstruction> rawInstructions, UInt32 ownOffset, Int32 targetOffset)
        {
            //Refactor using qsearch for searching and dictionary for caching
            Int32 index = -1;
            var offset = ownOffset + targetOffset + 1;
            for (int i = 0; i < rawInstructions.Count; i++)
                if (rawInstructions[i].Offset == offset)
                {
                    index = i;
                    break;
                }

            if (index == -1)
                throw new InvalidOperationException("Offset not found");

            return index;
        }

        private Int32 GetInstructionIndexByByteOffset(List<RawInstruction> rawInstructions, Int32 byteOffset)
        {
            //Refactor using qsearch for searching and dictionary for caching
            Int32 index = -1;
            for (int i = 0; i < rawInstructions.Count; i++)
                if ((rawInstructions[i].Offset <= byteOffset) && (byteOffset < rawInstructions[i].Offset + rawInstructions[i].Length))
                {
                    index = i;
                    break;
                }

            if (index == -1)
                throw new InvalidOperationException("Offset not found");

            return index;
        }

        public List<ILInstruction> ConvertRawInstructions(List<RawInstruction> rawInstructions)
        {
            List<ILInstruction> ilInstructions = new List<ILInstruction>();

            foreach (var rawInst in rawInstructions)
            {
                Object oper = rawInst.Operand;

                switch (rawInst.OpCode.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        oper = GetInstructionIndexByOffset(rawInstructions, rawInst.Offset, (Int32)rawInst.Operand + 4);
                        break;
                    case OperandType.ShortInlineBrTarget:
                        oper = GetInstructionIndexByOffset(rawInstructions, rawInst.Offset, (Int32)(SByte)rawInst.Operand + 1);
                        break;
                    case OperandType.InlineType:
                        {
                            var resType1 = MethodInfo.Module.ResolveType((Int32)rawInst.Operand, MethodInfo.DeclaringType.GetGenericArguments(), MethodInfo.GetGenericArguments());

                            break;
                        }
                    case OperandType.InlineTok:
                    case OperandType.InlineMethod:
                        {
                            var resType2 = MethodInfo.Module.ResolveMethod((Int32)rawInst.Operand, MethodInfo.DeclaringType.GetGenericArguments(), MethodInfo.GetGenericArguments());

                            break;
                        }
                }

                ILInstruction ilInst = new ILInstruction(rawInst.ByteCode, oper);
                ilInstructions.Add(ilInst);
            }
            return ilInstructions;
        }

        private ClassToken GetClassTokenForType(Type type)
        {
            ClassToken token = new ClassToken(type);
            return token;
        }

        public IEHTable CreateEHTable(IList<ExceptionHandlingClause> ehClauses)
        {
            EHTable = new EHTable(null);

            foreach (var clause in ehClauses)
            {
                Int32 tryFrom_OffsetIndex = GetInstructionIndexByByteOffset(RawInstructions, clause.TryOffset);
                Int32 tryTo_OffsetIndex = GetInstructionIndexByByteOffset(RawInstructions, clause.TryOffset + clause.TryLength - 1);

                Int32 handlerFrom_OffsetIndex = GetInstructionIndexByByteOffset(RawInstructions, clause.HandlerOffset);
                Int32 handlerTo_OffsetIndex = GetInstructionIndexByByteOffset(RawInstructions, clause.HandlerOffset + clause.HandlerLength - 1);

                switch (clause.Flags)
                {
                    case ExceptionHandlingClauseOptions.Clause:
                        {
                            var token = GetClassTokenForType(clause.CatchType);
                            EHTable.AddCatchHandler(tryFrom_OffsetIndex, tryTo_OffsetIndex, token, handlerFrom_OffsetIndex, handlerTo_OffsetIndex);
                            break;
                        }
                    case ExceptionHandlingClauseOptions.Finally:
                        {
                            EHTable.AddFinallyHandler(tryFrom_OffsetIndex, tryTo_OffsetIndex, handlerFrom_OffsetIndex, handlerTo_OffsetIndex);
                            break;
                        }
                    case ExceptionHandlingClauseOptions.Fault:
                    case ExceptionHandlingClauseOptions.Filter:
                        throw new NotImplementedException("Fault and Filter exception handling clause options are not implemented");
                }
            }
            return EHTable;
        }

        public LocalVarDescription[] ExtractLocals()
        {
            IList<LocalVariableInfo> locals = MethodInfo.GetMethodBody().LocalVariables;
              
            var localVarDescriptions = new LocalVarDescription[locals.Count];

            for(int i = 0; i< localVarDescriptions.Length; i++)
            {
                var local = locals.Single(x => x.LocalIndex == i);
                localVarDescriptions[i] = new LocalVarDescription()
                {
                    Name = null,
                    //Type= locals[i].LocalType
                    TypeToken = new ClassToken(local.LocalType),
                    IsPinned = local.IsPinned,
                    SType = ESSlotType.Val
                }; 
            }

            return localVarDescriptions; 
        }
 
        public MethodInheritanceAttributes ExtractInheritanceAttrs()
        {
            MethodInheritanceAttributes attrs = MethodInheritanceAttributes.NotSet;

            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Static))
                attrs = attrs | MethodInheritanceAttributes.Static;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Final))
                attrs = attrs | MethodInheritanceAttributes.Final;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Virtual))
                attrs = attrs | MethodInheritanceAttributes.Virtual;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.NewSlot))
                attrs = attrs | MethodInheritanceAttributes.NewSlot;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Abstract))
                attrs = attrs | MethodInheritanceAttributes.Abstract;

            return attrs;
        }

        public MethodOtherProps ExtractProperties()
        {
            MethodOtherProps props = MethodOtherProps.NotSet;

            if (MethodInfo.Attributes.HasFlag(MethodAttributes.UnmanagedExport))
                props = props | MethodOtherProps.UnmanagedExport;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.HideBySig))
                props = props | MethodOtherProps.HideBySig;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.VtableLayoutMask))
                props = props | MethodOtherProps.VtableLayoutMask;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.PinvokeImpl))
                props = props | MethodOtherProps.PinvokeImpl;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.CheckAccessOnOverride))
                props = props | MethodOtherProps.CheckAccessOnOverride;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.RTSpecialName))
                props = props | MethodOtherProps.RTSpecialName;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.HasSecurity))
                props = props | MethodOtherProps.HasSecurity;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.RequireSecObject))
                props = props | MethodOtherProps.RequireSecObject;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.ReservedMask))
                props = props | MethodOtherProps.ReservedMask;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.SpecialName))
                props = props | MethodOtherProps.SpecialName;

            return props;
        }

        public MemberAccessibility ExtractMemberAccessibility()
        {
            MemberAccessibility accessibility = default(MemberAccessibility);

            if (MethodInfo.Attributes.HasFlag(MethodAttributes.PrivateScope))
                accessibility = accessibility | MemberAccessibility.PrivateScope;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Private))
                accessibility = accessibility | MemberAccessibility.Private;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.FamANDAssem))
                accessibility = accessibility | MemberAccessibility.FamilyAndAssembly;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Assembly))
                accessibility = accessibility | MemberAccessibility.Assembly;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Family))
                accessibility = accessibility | MemberAccessibility.Family;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.FamORAssem))
                accessibility = accessibility | MemberAccessibility.FamilyOrAssembly;
            if (MethodInfo.Attributes.HasFlag(MethodAttributes.Public))
                accessibility = accessibility | MemberAccessibility.Public;

            return accessibility;
        }
          
        public MethodArgDescription[] ExtractArguments()
        {
            ParameterInfo[] paramInfoArray = MethodInfo.GetParameters();
            MethodArgDescription[] arguments = new MethodArgDescription[paramInfoArray.Length];
             
            for(int i=0;i< arguments.Length; i++)
            {
                var param = paramInfoArray.Single(x => x.Position == i);
                arguments[i] = new MethodArgDescription()
                {
                    Index = param.Position,
                    DefaultValue = param.DefaultValue,
                    HasDefaultValue = param.HasDefaultValue,
                    IsIn = param.IsIn,
                    IsLcid = param.IsLcid,
                    IsOptional = param.IsOptional,
                    IsOut = param.IsOut,
                    IsRetval = param.IsRetval,
                    Name = param.Name,
                    MetadataToken = param.MetadataToken,
                    TypeToken = new ClassToken(param.ParameterType), 
                }; 
            }

            return arguments;
        }  
 
    }
}
