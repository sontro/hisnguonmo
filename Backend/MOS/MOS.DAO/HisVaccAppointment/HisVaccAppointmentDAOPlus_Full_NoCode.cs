using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccAppointment
{
    public partial class HisVaccAppointmentDAO : EntityBase
    {
        public List<V_HIS_VACC_APPOINTMENT> GetView(HisVaccAppointmentSO search, CommonParam param)
        {
            List<V_HIS_VACC_APPOINTMENT> result = new List<V_HIS_VACC_APPOINTMENT>();
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

        public V_HIS_VACC_APPOINTMENT GetViewById(long id, HisVaccAppointmentSO search)
        {
            V_HIS_VACC_APPOINTMENT result = null;

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
