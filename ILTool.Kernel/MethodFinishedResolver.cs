
namespace ILTool.Kernel
{
    public class MethodFinishedResolver : IExecutionInterruptionResolver
    {
        private ICallStack _callStack;

        public MethodFinishedResolver(ICallStack callStack)
        {
            _callStack = callStack; 
        }

        public bool Resolve(MethodStateMachine methodSM)
        {
            //Pop away finished method
            _callStack.Pop();
            return false;
        }
    }
}
