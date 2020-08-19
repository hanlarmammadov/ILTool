
namespace ILTool.Kernel
{
    public interface IExecutionInterruptionResolver
    {
        bool Resolve(MethodStateMachine methodSM);
    }
}
