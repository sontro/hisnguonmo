using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleGet : BusinessBase
    {
        internal V_HIS_RATION_SCHEDULE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRationScheduleViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_RATION_SCHEDULE GetViewByCode(string code, HisRationScheduleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationScheduleDAO.GetViewByCode(code, filter.Query());
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
