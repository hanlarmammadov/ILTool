using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.ExceptionHandling
{
    public interface IEHTable
    {
        bool BelongsToTryBlock(Int32 instIndex);
        EHTryEntity GetFinallyForLeaveInst(Int32 instIndex);
        EHTryEntity GetNearestFinallyHandler(Int32 instIndex, Int32 index = 0);
        List<EHTryEntity> GetFinallyHandlers(Int32 instIndex, Int32 index = 0);
        EHTryEntity GetCatchIfExists(Int32 instIndex, ClassToken exToken);
        EHTable AddCatchHandler(Int32 tryFrom, Int32 tryTo, ClassToken exToken, Int32 handlerFrom, Int32 handlerTo);
        EHTable AddCatchHandler(string tryFromLabel, string tryToLabel, ClassToken exToken, string handlerFromLabel, string handlerToLabel);
        EHTable AddFinallyHandler(Int32 tryFrom, Int32 tryTo, Int32 handlerFrom, Int32 handlerTo);
        EHTable AddFinallyHandler(string tryFromLabel, string tryToLabel, string handlerFromLabel, string handlerToLabel);
        List<EHTryEntity> GetAllHandlers();
    }
}
