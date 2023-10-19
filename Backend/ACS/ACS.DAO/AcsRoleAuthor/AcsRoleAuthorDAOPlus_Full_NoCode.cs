using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleAuthor
{
    public partial class AcsRoleAuthorDAO : EntityBase
    {
        public List<V_ACS_ROLE_AUTHOR> GetView(AcsRoleAuthorSO search, CommonParam param)
        {
            List<V_ACS_ROLE_AUTHOR> result = new List<V_ACS_ROLE_AUTHOR>();
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

        public V_ACS_ROLE_AUTHOR GetViewById(long id, AcsRoleAuthorSO search)
        {
            V_ACS_ROLE_AUTHOR result = null;

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
