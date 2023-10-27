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
    }
}
