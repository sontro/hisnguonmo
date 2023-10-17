using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRole
{
    public partial class HisExecuteRoleDAO : EntityBase
    {
        public List<V_HIS_EXECUTE_ROLE> GetView(HisExecuteRoleSO search, CommonParam param)
        {
            List<V_HIS_EXECUTE_ROLE> result = new List<V_HIS_EXECUTE_ROLE>();
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

        public V_HIS_EXECUTE_ROLE GetViewById(long id, HisExecuteRoleSO search)
        {
            V_HIS_EXECUTE_ROLE result = null;

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
