using ILTool.Kernel.Descriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Metadata
{
    public class MethodToken : ImmutableToken
    {
        public readonly string Signature;
        //public readonly MethodType Type;
        public readonly string Assembly;
        public readonly ClassToken Owner;

        public MethodToken(/*MethodType type, */string assembly, string signature, ClassToken owner)
        {
            //Type = type;
            Assembly = assembly;
            Owner = owner;
            Signature = signature;

            SetHashCode(Signature.GetHashCode()/* + Type.GetHashCode()*/);
        } 
    }
}
