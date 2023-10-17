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

        public HIS_EXECUTE_ROLE_USER GetByCode(string code, HisExecuteRoleUserSO search)
        {
            HIS_EXECUTE_ROLE_USER result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_EXECUTE_ROLE_USER GetViewByCode(string code, HisExecuteRoleUserSO search)
        {
            V_HIS_EXECUTE_ROLE_USER result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_EXECUTE_ROLE_USER> GetDicByCode(HisExecuteRoleUserSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXECUTE_ROLE_USER> result = new Dictionary<string, HIS_EXECUTE_ROLE_USER>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
