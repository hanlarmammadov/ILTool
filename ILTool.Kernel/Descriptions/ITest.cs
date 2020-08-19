using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Descriptions
{ 
    public interface ITest
    {
        void Exec(object data);
        bool Success();
        string Result { get; }
    } 
}
