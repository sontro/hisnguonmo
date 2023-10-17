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
        internal V_HIS_EVENTS_CAUSES_DEATH GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEventsCausesDeathViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EVENTS_CAUSES_DEATH GetViewByCode(string code, HisEventsCausesDeathViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEventsCausesDeathDAO.GetViewByCode(code, filter.Query());
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
