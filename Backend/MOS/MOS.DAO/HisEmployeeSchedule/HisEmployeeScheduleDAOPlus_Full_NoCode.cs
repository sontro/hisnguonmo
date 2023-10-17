using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmployeeSchedule
{
    public partial class HisEmployeeScheduleDAO : EntityBase
    {
        public List<V_HIS_EMPLOYEE_SCHEDULE> GetView(HisEmployeeScheduleSO search, CommonParam param)
        {
            List<V_HIS_EMPLOYEE_SCHEDULE> result = new List<V_HIS_EMPLOYEE_SCHEDULE>();
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

        public V_HIS_EMPLOYEE_SCHEDULE GetViewById(long id, HisEmployeeScheduleSO search)
        {
            V_HIS_EMPLOYEE_SCHEDULE result = null;

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
