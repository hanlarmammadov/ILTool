using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Heap;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class MethodContext
    {
        #region Environment

        public IEvalStack EvalStack { get; set; }
        public Local[] Locals { get; set; }
        public MtdArg[] Args { get; set; }
        public IGCHeap GCHeap { get; set; }
        public ITypesHeap TypesHeap { get; set; }
        public ITypeLoader TypeLoader { get; set; }
        public bool ReturnsValue { get; set; }

        #endregion

        public static MethodContext CreateForMethod(MethodDesc method, IGCHeap gcHeap, ITypesHeap typesHeap, ITypeLoader typeLoader)
        {
            MethodContext context = new MethodContext();

            context.ReturnsValue = method.ReturnType != null;

            //GC heap
            context.GCHeap = gcHeap;

            //Type loader 
            context.TypeLoader = typeLoader;

            //Types heap
            context.TypesHeap = typesHeap;

            //Evaluation stack
            context.EvalStack = new EvalStack(method.MaxStackSize);

            //Prepare local variables 
            if (method.LocalVars != null && method.LocalVars.Length != 0)
            {
                context.Locals = new Local[method.LocalVars.Length];
                for (int i = 0; i < method.LocalVars.Length; i++)
                {
                    context.Locals[i] = new Local()
                    {
                        Description = method.LocalVars[i],
                        Val = null
                    };
                }
            }

            //Prepare arguments
            if (method.Arguments != null && method.Arguments.Length != 0)
            {
                context.Args = new MtdArg[method.Arguments.Length];
                for (int i = 0; i < method.Arguments.Length; i++)
                {
                    context.Args[i] = new MtdArg()
                    {
                        Description = method.Arguments[i],
                        Val = null
                    };
                }
            }

            return context;
        }
    }
}
