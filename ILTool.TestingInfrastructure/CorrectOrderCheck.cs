using ILTool.Kernel.Descriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.TestingInfrastructure
{
    public class CorrectOrderCheck : ITest
    {
        private int _previous;
        private bool _success = true;
        public StringBuilder _result = new StringBuilder();

        public string Result
        {
            get
            {
                return _result.ToString();
            }
        }

        public void Exec(object data)
        {
            _result.Append(data).Append(' ');

            if (!_success)
                return;

            int next = (int)data;
            if (next - _previous != 1)
                _success = false;
            else
                _previous = next;

        }

        public bool Success()
        {
            return _success;
        }
    }
}
