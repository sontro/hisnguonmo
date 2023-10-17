using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepartment
{
    public partial class HisDepartmentDAO : EntityBase
    {
        public List<V_HIS_DEPARTMENT> GetView(HisDepartmentSO search, CommonParam param)
        {
            List<V_HIS_DEPARTMENT> result = new List<V_HIS_DEPARTMENT>();
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

        public V_HIS_DEPARTMENT GetViewById(long id, HisDepartmentSO search)
        {
            V_HIS_DEPARTMENT result = null;

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
