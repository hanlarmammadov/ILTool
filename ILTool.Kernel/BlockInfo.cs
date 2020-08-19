using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public enum BlockType
    {
        Finally = 0,
        Catch,
        Leave
    }

    public class BlockInfo
    {
        public Int32 JumpIndex { get; private set; }
        public BlockType Type { get; private set; }
        public ExeptionInfo ExInfo { get; private set; }

        protected BlockInfo() { }

        public static BlockInfo Finally(Int32 jumpIndex)
        {
            return new BlockInfo()
            {
                Type = BlockType.Finally,
                JumpIndex = jumpIndex,
            };
        }

        public static BlockInfo Catch(Int32 jumpIndex, ExeptionInfo exInfo)
        {
            return new BlockInfo()
            {
                Type = BlockType.Catch,
                JumpIndex = jumpIndex,
                ExInfo = exInfo
            };
        }

        public static BlockInfo Leave(Int32 jumpIndex)
        {
            return new BlockInfo()
            {
                Type = BlockType.Leave,
                JumpIndex = jumpIndex,
            };
        }
    }
}
