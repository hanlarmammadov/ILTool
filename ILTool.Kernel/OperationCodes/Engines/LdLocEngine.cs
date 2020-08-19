using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.BaseClasses;
using ILTool.Kernel.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.OperationCodes.Engines
{
    public class LdLocEngine : PrimitivesEngine, IILOperationEngine
    {
        public virtual void Execute(MethodContext context, MethodState state, object localInx = null)
        {  
            if (localInx == null || !((localInx is Int32) || (localInx is Byte)))
                throw new OperandsNotSupportedByOperationException(ByteCode.Stloc, localInx);

            Local local = context.Locals[(localInx is Int32) ? (Int32)localInx : (Int32)(Byte)localInx];
             
            var slot = new ESSlot()
            { 
                TypeToken = local.Description.TypeToken,
                SType = local.Description.SType
            };

            if (local.Description.TypeToken.IsPrimitive)
            {
                if (!IsPrimaryType(local.Description.TypeToken.PrimitiveType))
                    slot.Val = GetUnaryOperation(local.Description.TypeToken.PrimitiveType, UnaryPrimitiveOpType.GetStackRep)(local.Val);
                else
                    slot.Val = local.Val;
            }
            else
                slot.Val = local.Val;
            
            context.EvalStack.Push(slot);
        } 
    }
}
