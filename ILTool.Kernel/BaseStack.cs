using ILTool.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public abstract class BaseStack<TItem> : IStackBase<TItem>
    {
        protected Int32 _maxSize;
        protected Stack<TItem> _stack;

        protected abstract MaxStackSizeException GetMaxStackSizeException();

        protected BaseStack(Int32 maxSize)
        {
            _maxSize = maxSize;
            _stack = new Stack<TItem>(_maxSize);
        }

        public virtual void Push(TItem item)
        {
            if (Count == _maxSize)
                throw GetMaxStackSizeException();
            _stack.Push(item);
        }

        public virtual TItem Pop()
        {
            return _stack.Pop();
        }

        public virtual TItem Peek()
        {
            return _stack.Peek();
        }

        public void Clear()
        {
            _stack.Clear(); 
        }

        public virtual Int32 Count
        {
            get
            {
                return _stack.Count;
            }
        }
    }
}
