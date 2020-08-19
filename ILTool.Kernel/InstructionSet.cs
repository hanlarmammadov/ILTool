using ILTool.Kernel.Exceptions;
using ILTool.Kernel.OperationCodes.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{ 
    public class ILOperationSet : IILOperationSet
    {
        private static Dictionary<ByteCode, IILOperationEngine> _engines;

        static ILOperationSet()
        { 
            _engines = new Dictionary<ByteCode, IILOperationEngine>();

            //Constants
            _engines.Add(ByteCode.Ldc_I4, new Ldc_I4Engine());
            _engines.Add(ByteCode.Ldc_I4_S, new Ldc_I4_sEngine());
            _engines.Add(ByteCode.Ldc_I4_0, new Ldc_I4_0Engine());
            _engines.Add(ByteCode.Ldc_I4_1, new Ldc_I4_1Engine());
            _engines.Add(ByteCode.Ldc_I4_2, new Ldc_I4_2Engine());
            _engines.Add(ByteCode.Ldc_I4_3, new Ldc_I4_3Engine());
            _engines.Add(ByteCode.Ldc_I4_4, new Ldc_I4_4Engine());
            _engines.Add(ByteCode.Ldc_I4_5, new Ldc_I4_5Engine());
            _engines.Add(ByteCode.Ldc_I4_6, new Ldc_I4_6Engine());
            _engines.Add(ByteCode.Ldc_I4_7, new Ldc_I4_7Engine());
            _engines.Add(ByteCode.Ldc_I4_8, new Ldc_I4_8Engine());
            _engines.Add(ByteCode.Ldc_I4_M1, new Ldc_I4_M1Engine());

            //Arithmetics and logic
            _engines.Add(ByteCode.Add, new AddEngine());
            _engines.Add(ByteCode.Mul, new MulEngine());
            _engines.Add(ByteCode.Sub, new SubEngine());
            _engines.Add(ByteCode.Not, new NotEngine());
            _engines.Add(ByteCode.Or, new OrEngine());
            _engines.Add(ByteCode.And, new AndEngine());
            _engines.Add(ByteCode.Xor, new XorEngine());
            _engines.Add(ByteCode.Ceq, new CeqEngine());
            
            //Locals
            _engines.Add(ByteCode.Ldloc, new LdLocEngine());
            _engines.Add(ByteCode.Ldloc_S, new LdLoc_sEngine());
            _engines.Add(ByteCode.Ldloc_0, new LdLoc_0Engine());
            _engines.Add(ByteCode.Ldloc_1, new LdLoc_1Engine());
            _engines.Add(ByteCode.Ldloc_2, new LdLoc_2Engine());
            _engines.Add(ByteCode.Ldloc_3, new LdLoc_3Engine()); 
            _engines.Add(ByteCode.Stloc, new StLocEngine());
            _engines.Add(ByteCode.Stloc_S, new StLoc_sEngine());
            _engines.Add(ByteCode.Stloc_0, new StLoc_0Engine());
            _engines.Add(ByteCode.Stloc_1, new StLoc_1Engine());
            _engines.Add(ByteCode.Stloc_2, new StLoc_2Engine());
            _engines.Add(ByteCode.Stloc_3, new StLoc_3Engine());

            //Arguments
            _engines.Add(ByteCode.Ldarg, new LdargEngine());
            _engines.Add(ByteCode.Ldarg_S, new Ldarg_sEngine());
            _engines.Add(ByteCode.Ldarg_0, new Ldarg_0Engine());
            _engines.Add(ByteCode.Ldarg_1, new Ldarg_1Engine());
            _engines.Add(ByteCode.Ldarg_2, new Ldarg_2Engine());
            _engines.Add(ByteCode.Ldarg_3, new Ldarg_3Engine());

            //Method call and return
            _engines.Add(ByteCode.Ret, new RetEngine());
            _engines.Add(ByteCode.Call, new CallEngine());
            _engines.Add(ByteCode.Callvirt, new CallvirtEngine());

            //Branching
            _engines.Add(ByteCode.Br, new BrEngine());
            _engines.Add(ByteCode.Br_S, new Br_sEngine());
            _engines.Add(ByteCode.Brfalse, new BrfalseEngine());
            _engines.Add(ByteCode.Brfalse_S, new Brfalse_sEngine());
            _engines.Add(ByteCode.Brtrue, new BrtrueEngine());
            _engines.Add(ByteCode.Brtrue_S, new Brtrue_sEngine()); 
            _engines.Add(ByteCode.Beq, new BeqEngine()); 
            _engines.Add(ByteCode.Bge, new BgeEngine());
            _engines.Add(ByteCode.Cgt, new CgtEngine());

            //Heap, boxing and unboxing
            _engines.Add(ByteCode.Box, new BoxEngine());
            _engines.Add(ByteCode.Unbox, new UnboxEngine());
            _engines.Add(ByteCode.Newobj, new NewobjEngine());
            _engines.Add(ByteCode.Ldnull, new LdnullEngine());

            //Other
            _engines.Add(ByteCode.Pop, new PopEngine());
            _engines.Add(ByteCode.Nop, new NopEngine());

            //Conversions
            _engines.Add(ByteCode.Conv_I1, new Conv_I1Engine());
            _engines.Add(ByteCode.Conv_U1, new Conv_U1Engine());
            _engines.Add(ByteCode.Conv_I2, new Conv_I2Engine());
            _engines.Add(ByteCode.Conv_U2, new Conv_U2Engine());
            _engines.Add(ByteCode.Conv_I4, new Conv_I4Engine());
            _engines.Add(ByteCode.Conv_U4, new Conv_U4Engine());
            _engines.Add(ByteCode.Conv_I8, new Conv_I8Engine());
            _engines.Add(ByteCode.Conv_U8, new Conv_U8Engine());
            _engines.Add(ByteCode.Conv_R4, new Conv_R4Engine());
            _engines.Add(ByteCode.Conv_R8, new Conv_R8Engine());

            //Exceptions 
            _engines.Add(ByteCode.Throw, new ThrowEngine());
            _engines.Add(ByteCode.Leave, new LeaveEngine());
            _engines.Add(ByteCode.Leave_S, new Leave_sEngine());
            _engines.Add(ByteCode.Endfinally, new EndfinallyEngine());
        }

        public IILOperationEngine GetEngine(ByteCode code)
        {
            if (_engines.ContainsKey(code))
                return _engines[code];
            else throw new OpCodeNotImplementedException(code, "IL operation has not been implemented");
        }
    }
}
