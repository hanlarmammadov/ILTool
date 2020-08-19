using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class LdargEngine : PrimitivesEngine, IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object operand = null)
        {
            MtdArg arg = context.Args[(Int32)operand];
            ESSlot slot = new ESSlot()
            {
                TypeToken = arg.Description.TypeToken,
                SType = arg.Description.SType
            };
            
            if (arg.Description.TypeToken.IsPrimitive)
                slot.Val = GetUnaryOperation(arg.Description.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStackRep)(arg.Val);
            else
                slot.Val = arg.Val;
            
            context.EvalStack.Push(slot);
        }
    }
}
