using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleAuthor
{
    public partial class AcsRoleAuthorDAO : EntityBase
    {
        public ACS_ROLE_AUTHOR GetByCode(string code, AcsRoleAuthorSO search)
        {
            ACS_ROLE_AUTHOR result = null;

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

        public Dictionary<string, ACS_ROLE_AUTHOR> GetDicByCode(AcsRoleAuthorSO search, CommonParam param)
        {
            Dictionary<string, ACS_ROLE_AUTHOR> result = new Dictionary<string, ACS_ROLE_AUTHOR>();
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
