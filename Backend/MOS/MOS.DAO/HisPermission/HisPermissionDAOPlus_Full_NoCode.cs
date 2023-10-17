using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPermission
{
    public partial class HisPermissionDAO : EntityBase
    {
        public List<V_HIS_PERMISSION> GetView(HisPermissionSO search, CommonParam param)
        {
            List<V_HIS_PERMISSION> result = new List<V_HIS_PERMISSION>();
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

        public V_HIS_PERMISSION GetViewById(long id, HisPermissionSO search)
        {
            V_HIS_PERMISSION result = null;

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
