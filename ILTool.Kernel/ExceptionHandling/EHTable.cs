using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.ExceptionHandling
{
    public class EHTable : IEHTable
    {
        private List<EHTryEntity> _table;
        private Int32 _currIndex;
        private ILabelResolver _labelResolver;

        public EHTable(ILabelResolver labelResolver)
        {
            _table = new List<EHTryEntity>();
            _currIndex = 1;
            _labelResolver = labelResolver;
        }

        public bool BelongsToTryBlock(Int32 instIndex)
        {
            return _table.Where(x => x.IncludesInstruction(instIndex)).FirstOrDefault() != null;
        }

        public EHTryEntity GetFinallyForLeaveInst(Int32 instIndex)
        {
            return _table.Where(x => x.HandlerType == EHHandlerType.Finally).Where(x => x.IsLastInstruction(instIndex)).SingleOrDefault();
        }

        public EHTryEntity GetNearestFinallyHandler(Int32 instIndex, Int32 index = 0)
        {
            var q = _table.Where(x => x.HandlerType == EHHandlerType.Finally).Where(x => x.IncludesInstruction(instIndex));
            if (index != 0)
                q = q.Where(x => x.Index < index);
            return q.FirstOrDefault();
        }

        public List<EHTryEntity> GetFinallyHandlers(Int32 instIndex, Int32 index = 0)
        {
            var q = _table.Where(x => x.HandlerType == EHHandlerType.Finally).Where(x => x.IncludesInstruction(instIndex));
            if (index != 0)
                q = q.Where(x => x.Index < index);
            return q.Reverse().ToList();
            //return _table.Where(x => x.HandlerType == EHHandlerType.Finally).Where(x => x.IncludesInstruction(instIndex)).ToList();
        }

        public EHTryEntity GetCatchIfExists(Int32 instIndex, ClassToken exToken)
        {
            return _table.Where(x => x.HandlerType == EHHandlerType.Catch).Where(x => x.ExceptionToken.Equals(exToken)).Where(x => x.IncludesInstruction(instIndex)).FirstOrDefault();
        } 

        public EHTable AddCatchHandler(Int32 tryFrom, Int32 tryTo, ClassToken exToken, Int32 handlerFrom, Int32 handlerTo)
        {
            _table.Add(new EHTryEntity()
            {
                HandlerType = EHHandlerType.Catch,
                TryOffset = tryFrom,
                TryLength = tryTo - tryFrom,
                ExceptionToken = exToken,
                HandlerOffset = handlerFrom,
                HandlerLength = handlerTo - handlerFrom,
                Index = _currIndex++
            });
            return this;
        }

        public EHTable AddCatchHandler(string tryFromLabel, string tryToLabel, ClassToken exToken, string handlerFromLabel, string handlerToLabel)
        {
            return AddCatchHandler(_labelResolver.GetNumber(tryFromLabel), _labelResolver.GetNumber(tryToLabel), exToken, _labelResolver.GetNumber(handlerFromLabel), _labelResolver.GetNumber(handlerToLabel));
        }
       
        public EHTable AddFinallyHandler(Int32 tryFrom, Int32 tryTo, Int32 handlerFrom, Int32 handlerTo)
        {
            _table.Add(new EHTryEntity()
            {
                HandlerType = EHHandlerType.Finally,
                TryOffset = tryFrom,
                TryLength = tryTo - tryFrom,
                HandlerOffset = handlerFrom,
                HandlerLength = handlerTo - handlerFrom,
                Index = _currIndex++
            });
            return this;
        }

        public EHTable AddFinallyHandler(string tryFromLabel, string tryToLabel, string handlerFromLabel, string handlerToLabel)
        {
            return AddFinallyHandler(_labelResolver.GetNumber(tryFromLabel), _labelResolver.GetNumber(tryToLabel), _labelResolver.GetNumber(handlerFromLabel), _labelResolver.GetNumber(handlerToLabel));
        }

        public List<EHTryEntity> GetAllHandlers()
        {
            return _table; 
        }
    }
}
