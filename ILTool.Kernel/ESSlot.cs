using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using System;
namespace ILTool.Kernel
{
    public enum ESSlotType
    {
        Val = 0,        //Value
        HORef = 1,      //Heap object reference
        MPtr = 2        //Managed pointer
    }

    public class ESSlot
    {
        //public TypeDesc CLRType { get; set; }
        public ClassToken TypeToken { get; set; }
        public Object Val { get; set; }
        public ESSlotType SType { get; set; }

        public ESSlot() { }
        public ESSlot(ESSlotType slotType)
        {
            SType = slotType;
        }
        public ESSlot(ESSlotType slotType, ClassToken typeToken)
        {
            SType = slotType;
            TypeToken = typeToken;
        }
    }
}
