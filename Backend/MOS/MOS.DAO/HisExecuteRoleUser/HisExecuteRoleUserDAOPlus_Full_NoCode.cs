using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRoleUser
{
    public partial class HisExecuteRoleUserDAO : EntityBase
    {
        public List<V_HIS_EXECUTE_ROLE_USER> GetView(HisExecuteRoleUserSO search, CommonParam param)
        {
            List<V_HIS_EXECUTE_ROLE_USER> result = new List<V_HIS_EXECUTE_ROLE_USER>();
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

        public V_HIS_EXECUTE_ROLE_USER GetViewById(long id, HisExecuteRoleUserSO search)
        {
            V_HIS_EXECUTE_ROLE_USER result = null;

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
