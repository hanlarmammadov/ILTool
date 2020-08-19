using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Domain
{
    public class TypeObject
    {
        public VTable VTable;
        public TypeDesc TypeDesc;
        public ClassToken Token;
        //public Int32 HeapObjAddr;

        public TypeObject(ClassToken token, TypeDesc typeDesc, VTable vTable)
        {
            TypeDesc = typeDesc;
            VTable = vTable;
            Token = token;
        }
    }
}
