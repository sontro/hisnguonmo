using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCaroDepartment
{
    public partial class HisCaroDepartmentDAO : EntityBase
    {
        public List<V_HIS_CARO_DEPARTMENT> GetView(HisCaroDepartmentSO search, CommonParam param)
        {
            List<V_HIS_CARO_DEPARTMENT> result = new List<V_HIS_CARO_DEPARTMENT>();
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

        public V_HIS_CARO_DEPARTMENT GetViewById(long id, HisCaroDepartmentSO search)
        {
            V_HIS_CARO_DEPARTMENT result = null;

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
