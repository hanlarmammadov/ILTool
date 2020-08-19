using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public enum FlowInterruption
    {
        Next = 0,
        LeaveBlock = 4,
        EndFinally = 5,
    }

    public enum ExecutionInterruption : Int32
    {
        None = 0,
        Call = 1,
        Return = 2,
        Throw = 3,
        Finished = 4
    }

    public class MethodState
    {
        public MethodState()
        {
            CurrInstIdx = -1;
            JumpIndex = -1;
            FlowInterruption = FlowInterruption.Next;
            ExecutionInterruption = ExecutionInterruption.None;
            BlockJump = new Stack<BlockInfo>();
        }

        public Int32 CurrInstIdx { get; set; }
        public FlowInterruption FlowInterruption { get; set; }
        public ExecutionInterruption ExecutionInterruption { get; set; }
        public bool MethodPendingFinallies { get; set; }
        public Int32 LeaveRequest { get; set; }

        //Interruption requests
        public Int32 JumpIndex { get; set; }
        public MethodDesc CallMethod { get; set; }
        public Stack<BlockInfo> BlockJump { get; set; }
        public ExeptionInfo ExeptionInfo { get; set; } 
        // 
    }
}
