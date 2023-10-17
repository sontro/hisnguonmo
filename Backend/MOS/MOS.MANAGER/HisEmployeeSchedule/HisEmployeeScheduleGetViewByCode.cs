using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    partial class HisEmployeeScheduleGet : BusinessBase
    {
        internal V_HIS_EMPLOYEE_SCHEDULE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEmployeeScheduleViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EMPLOYEE_SCHEDULE GetViewByCode(string code, HisEmployeeScheduleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeScheduleDAO.GetViewByCode(code, filter.Query());
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
