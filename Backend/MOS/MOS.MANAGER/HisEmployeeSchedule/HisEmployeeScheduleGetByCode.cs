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
        internal HIS_EMPLOYEE_SCHEDULE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEmployeeScheduleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EMPLOYEE_SCHEDULE GetByCode(string code, HisEmployeeScheduleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeScheduleDAO.GetByCode(code, filter.Query());
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
