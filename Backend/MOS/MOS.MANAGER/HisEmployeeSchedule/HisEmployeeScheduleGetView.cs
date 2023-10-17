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
        internal List<V_HIS_EMPLOYEE_SCHEDULE> GetView(HisEmployeeScheduleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEmployeeScheduleDAO.GetView(filter.Query(), param);
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
