using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmployee
{
    public partial class HisEmployeeDAO : EntityBase
    {
        public List<V_HIS_EMPLOYEE> GetView(HisEmployeeSO search, CommonParam param)
        {
            List<V_HIS_EMPLOYEE> result = new List<V_HIS_EMPLOYEE>();
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

        public V_HIS_EMPLOYEE GetViewById(long id, HisEmployeeSO search)
        {
            V_HIS_EMPLOYEE result = null;

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
