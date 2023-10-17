using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathGet : BusinessBase
    {
        internal HIS_EVENTS_CAUSES_DEATH GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEventsCausesDeathFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EVENTS_CAUSES_DEATH GetByCode(string code, HisEventsCausesDeathFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEventsCausesDeathDAO.GetByCode(code, filter.Query());
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
