using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Domain
{
    public class VTable
    {
        private Dictionary<MethodToken, MethodDesc> _table;
        private IInterfaceOffsetTable _iotTable;
          
        public VTable(Dictionary<MethodToken, MethodDesc> table, IInterfaceOffsetTable iotTable)
        {
            _table = table;
            _iotTable = iotTable;
        }

        public Dictionary<MethodToken, MethodDesc> GetTable()
        {
            return _table;
        }

        public MethodDesc GetMethod(MethodToken methodMetadata)
        {
            MethodDesc res = null;
            if (_table.ContainsKey(methodMetadata))
                res = _table[methodMetadata];
            return res;
        }

        //public MethodDesc GetMethod(string methodName, string interfaceName)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
