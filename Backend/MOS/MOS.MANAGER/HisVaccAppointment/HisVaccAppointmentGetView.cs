using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    partial class HisVaccAppointmentGet : BusinessBase
    {
        internal List<V_HIS_VACC_APPOINTMENT> GetView(HisVaccAppointmentViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisVaccAppointmentDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_VACC_APPOINTMENT> GetViewByIds(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                HisVaccAppointmentViewFilterQuery filter = new HisVaccAppointmentViewFilterQuery();
                filter.IDs = ids;
                return this.GetView(filter);
            }
            return null;
        }
    }
}
