using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleRole
{
    public partial class AcsModuleRoleDAO : EntityBase
    {
        public List<V_ACS_MODULE_ROLE> GetView(AcsModuleRoleSO search, CommonParam param)
        {
            List<V_ACS_MODULE_ROLE> result = new List<V_ACS_MODULE_ROLE>();
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

        public V_ACS_MODULE_ROLE GetViewById(long id, AcsModuleRoleSO search)
        {
            V_ACS_MODULE_ROLE result = null;

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
