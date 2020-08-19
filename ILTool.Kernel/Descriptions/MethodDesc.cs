using ILTool.Kernel.ExceptionHandling;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Descriptions
{
    public delegate void InstructionExecuted(string data);

    public class MethodDesc : DescBase
    {
        public string Name { get; set; }
        public string Assembly { get; set; }
        public string OwnerName { get; set; }
        //public MethodType Type { get; set; }
        public ILInstruction[] Instructions { get; set; }
        //public bool ReturnsValue { get; set; }
        public bool IsEntryPoint { get; set; }
        public Int32 MaxStackSize { get; set; }
        public MethodArgDescription[] Arguments { get; set; }
        public LocalVarDescription[] LocalVars { get; set; }
        public IEHTable EHTable { get; set; }
        /*public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsNew { get; set; }
        public bool IsFinal { get; set; }   //Sealed
        public bool IsAbstract { get; set; }*/
        public TypeDesc Owner { get; set; }
        private ILabelResolver _labelResolver;

        public MemberAccessibility Accessibility { get; set; }
        public MethodOtherProps Props { get; set; }
        public MethodInheritanceAttributes InheritanceAttributes { get; set; }
        public ClassToken ReturnType { get; set; }

        private ITest _test;

        public bool TakesArguments
        {
            get
            { 
                return (Arguments != null || Props.HasFlag(MethodOtherProps.SpecialName) && Name == ".Ctor");  //Ctor always take 'this' implicitly
            }
        }

        public MethodToken Metadata
        {
            get
            {
                return new MethodToken(/*this.Type,*/ this.Assembly, this.Name, this.Owner?.Metadata);
            }
        }

        #region Builder

        public MethodDesc()
        {
            _labelResolver = new LabelResolver();
            EHTable = new EHTable(_labelResolver);
        } 
        //public MethodDesc(string name, MethodType type, Int32 maxStackSize) : this()
        //{
        //    Name = name;
        //    Type = type;
        //    MaxStackSize = maxStackSize;
        //} 
        public MethodDesc(string name, Int32 maxStackSize) : this()
        {
            Name = name; 
            MaxStackSize = maxStackSize;
        } 
        public MethodDesc AddCallback(ITest test)
        {
            _test = test;
            return this;
        } 
        public MethodDesc AddArguments(MethodArgDescription[] arguments)
        {
            Arguments = arguments;
            return this;
        }
        public MethodDesc AddLocals(LocalVarDescription[] locals)
        {
            LocalVars = locals;
            return this;
        }
        public MethodDesc AddInstructions(ILInstruction[] instructions)
        {
            _labelResolver.ResolveInstructionLabels(instructions);

            //Add callback if provided
            if (_test != null)
                foreach (var inst in instructions)
                    inst.AddCallback(_test);
            //

            Instructions = instructions;
            return this;
        }
        public MethodDesc AddInheritanceAttributeFlag(MethodInheritanceAttributes flag)
        {
            InheritanceAttributes = InheritanceAttributes | flag;
            return this;
        }

        //public MethodDesc Virtual()
        //{
        //    IsVirtual = true;
        //    return this;
        //}
        //public MethodDesc Override()
        //{
        //    IsOverride = true;
        //    return this;
        //}
        //public MethodDesc New()
        //{ 
        //    IsNew = true;
        //    return this;
        //}

        //public MethodDesc Final()
        //{
        //    IsFinal = true;
        //    return this;
        //}
        //public MethodDesc Abstract()
        //{
        //    IsAbstract = true;
        //    return this;
        //}

        public MethodDesc EntryPoint()
        {
            IsEntryPoint = true;
            return this;
        }
        //public MethodDesc Returns()
        //{
        //    ReturnsValue = true;
        //    return this;
        //}
        public MethodDesc BelongsTo(TypeDesc owner)
        {
            Owner = owner;
            return this;
        }
        public MethodDesc AddInheritanceAttributes(MethodInheritanceAttributes attrs)
        {
            this.InheritanceAttributes = attrs;
            return this;
        }
        public MethodDesc AddPropertiesFlag(MethodOtherProps flag)
        {
            Props = Props | flag;
            return this;
        }
        public MethodDesc AddProperties(MethodOtherProps props)
        {
            Props = props;
            return this;
        }
        public MethodDesc AddMemberAccessibility(MemberAccessibility attrs)
        {
            this.Accessibility = attrs;
            return this;
        }
        public MethodDesc AddEHTable(IEHTable ehTable)
        {
            this.EHTable = ehTable;
            return this;
        }
        public MethodDesc AddReturnType(ClassToken returnType)
        {
            //ReturnsValue = true;
            ReturnType = returnType;
            return this;
        }
        #endregion

    }
}
