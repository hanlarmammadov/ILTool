using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Metadata
{
    public abstract class ImmutableToken
    {
        protected Int32 _hashCode;

        protected void SetHashCode(Int32 hashCode)
        {
            _hashCode = hashCode;
        }

        public override bool Equals(object obj)
        {
            var immutObj = (obj as ImmutableToken);
            if (immutObj == null)
                return false;

            return _hashCode == immutObj._hashCode;
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public static bool operator ==(ImmutableToken obj1, ImmutableToken obj2)
        {
            if (Object.ReferenceEquals(obj2, null) || Object.ReferenceEquals(obj1, null))
                return false;

            return obj1._hashCode == obj2._hashCode;
        }

        public static bool operator !=(ImmutableToken obj1, ImmutableToken obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
