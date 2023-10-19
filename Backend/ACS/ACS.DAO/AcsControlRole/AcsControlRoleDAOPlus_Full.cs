using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsControlRole
{
    public partial class AcsControlRoleDAO : EntityBase
    {
        public List<V_ACS_CONTROL_ROLE> GetView(AcsControlRoleSO search, CommonParam param)
        {
            List<V_ACS_CONTROL_ROLE> result = new List<V_ACS_CONTROL_ROLE>();

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

        public ACS_CONTROL_ROLE GetByCode(string code, AcsControlRoleSO search)
        {
            ACS_CONTROL_ROLE result = null;

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
        
        public V_ACS_CONTROL_ROLE GetViewById(long id, AcsControlRoleSO search)
        {
            V_ACS_CONTROL_ROLE result = null;

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

        public V_ACS_CONTROL_ROLE GetViewByCode(string code, AcsControlRoleSO search)
        {
            V_ACS_CONTROL_ROLE result = null;

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

        public Dictionary<string, ACS_CONTROL_ROLE> GetDicByCode(AcsControlRoleSO search, CommonParam param)
        {
            Dictionary<string, ACS_CONTROL_ROLE> result = new Dictionary<string, ACS_CONTROL_ROLE>();
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
