using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Descriptions
{ 


    public class TypeDesc : DescBase
    { 
        public string Name { get; set; } 
        public List<InterfaceDesc> ImplementedInterfaces { get; set; }
        public TypeDesc BaseClass { get; set; }
        public List<MethodDesc> Methods { get; set; }
        public Boolean IsValueType { get; set; }
        public Boolean IsPrimitive { get; set; }

        public bool IsOfTypeOrDerivedFrom(ClassToken token)
        {
            bool result = false;
            TypeDesc curr = this;
            do
            {
                if (curr.Metadata.Equals( token))
                {
                    result = true;
                    break;
                }
                else
                    curr = curr.BaseClass; 
            }
            while (curr != null);
            return result;
        }

        //Temporary
        public ClassToken Metadata
        {
            get
            {
                return new ClassToken(this.Name, IsPrimitive);
            }
        }
        //

        public TypeDesc()
        {
            ImplementedInterfaces = new List<InterfaceDesc>();
            Methods = new List<MethodDesc>();
        }

        //Primitive type's operations
        //public BinaryOperation Add;
        //public BinaryOperation Sub;
        //public BinaryOperation Mul;
        //public BinaryOperation And;
        //public BinaryOperation Or;
        //public BinaryOperation Xor;
        //public UnaryOperation Not;
        //public BinaryOperation Equal;
        //public BinaryOperation NotEqual;

    }
}
