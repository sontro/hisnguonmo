using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSumStt
{
    partial class HisRationSumSttGet : BusinessBase
    {
        internal HIS_RATION_SUM_STT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRationSumSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SUM_STT GetByCode(string code, HisRationSumSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationSumSttDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
