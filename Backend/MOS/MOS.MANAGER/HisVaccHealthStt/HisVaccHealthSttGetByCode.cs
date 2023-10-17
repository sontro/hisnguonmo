using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccHealthStt
{
    partial class HisVaccHealthSttGet : BusinessBase
    {
        internal HIS_VACC_HEALTH_STT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisVaccHealthSttFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_VACC_HEALTH_STT GetByCode(string code, HisVaccHealthSttFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccHealthSttDAO.GetByCode(code, filter.Query());
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
