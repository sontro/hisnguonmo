using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAppointmentServ
{
    public partial class HisAppointmentServDAO : EntityBase
    {
        public List<V_HIS_APPOINTMENT_SERV> GetView(HisAppointmentServSO search, CommonParam param)
        {
            List<V_HIS_APPOINTMENT_SERV> result = new List<V_HIS_APPOINTMENT_SERV>();
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

        public V_HIS_APPOINTMENT_SERV GetViewById(long id, HisAppointmentServSO search)
        {
            V_HIS_APPOINTMENT_SERV result = null;

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
