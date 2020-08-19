using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Domain;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ILTool.Kernel.Heap
{
    public class GCHeap : IGCHeap
    {
        private Dictionary<Int32, GCHeapObj> _htable;
        //private Dictionary<TypeDesc, HeapObj> _loadedTypesTable;
        //private string _domainName;
        //private ITypeLoader _typeLoader;
        private Int32 _nxtfreeSlt;
        private static readonly object htableLocker = new object();
        protected GCHeap() { }

        public GCHeapObj AllocObj(TypeObject typeObject)
        { 
            GCHeapObj heapObj = new GCHeapObj()
            {
                Addr = _nxtfreeSlt,
                TypeToken = typeObject.Token,
                Val = null
            };

            _htable.Add(_nxtfreeSlt, heapObj);
            Interlocked.Increment(ref _nxtfreeSlt);
            return heapObj;
        }

        public Int32 AllocObjRetAddr(TypeObject typeObject)
        {
           return AllocObj(typeObject).Addr;
        }

        //public GCHeapObj AllocObjType()
        //{
        //    return AllocObj(null);
        //}

        public GCHeapObj GetObj(Int32 addr)
        {
            GCHeapObj obj = null;
            lock (htableLocker)
            {
                if (_htable.ContainsKey(addr))
                    obj = _htable[addr];

                if (obj != null && !obj.IsGCollected)
                    return obj;
                else return GCHeapObj.NullObj;
            }
        }

        //public HeapObj GetObjType(Int32 typeAddr)
        //{
        //    return GetObj(typeAddr);
        //}

        //public GCHeapObj GetTypeObjForObj(Int32 objAddr)
        //{
        //    return GetObj(GetObj(objAddr).TypeObjAddr);
        //}

        public class Factory : IGCHeapFactory
        {
            public IGCHeap Create()
            {
                GCHeap heap = new GCHeap()
                {
                    _htable = new Dictionary<int, GCHeapObj>(),
                    _nxtfreeSlt = 1
                };
                return heap;
            }
        }
    }
}
