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
        internal HIS_RATION_SCHEDULE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRationScheduleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_RATION_SCHEDULE GetByCode(string code, HisRationScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationScheduleDAO.GetByCode(code, filter.Query());
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
