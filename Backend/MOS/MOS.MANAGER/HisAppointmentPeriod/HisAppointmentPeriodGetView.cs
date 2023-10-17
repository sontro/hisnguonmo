using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    partial class HisAppointmentPeriodGet : BusinessBase
    {
        internal List<V_HIS_APPOINTMENT_PERIOD> GetView(HisAppointmentPeriodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAppointmentPeriodDAO.GetView(filter.Query(), param);
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
