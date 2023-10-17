using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentPeriod
{
    public partial class HisAppointmentPeriodDAO : EntityBase
    {
        public List<V_HIS_APPOINTMENT_PERIOD> GetView(HisAppointmentPeriodSO search, CommonParam param)
        {
            List<V_HIS_APPOINTMENT_PERIOD> result = new List<V_HIS_APPOINTMENT_PERIOD>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_APPOINTMENT_PERIOD GetViewById(long id, HisAppointmentPeriodSO search)
        {
            V_HIS_APPOINTMENT_PERIOD result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
