using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleRole
{
    public partial class AcsModuleRoleDAO : EntityBase
    {
        public ACS_MODULE_ROLE GetByCode(string code, AcsModuleRoleSO search)
        {
            ACS_MODULE_ROLE result = null;

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

        public Dictionary<string, ACS_MODULE_ROLE> GetDicByCode(AcsModuleRoleSO search, CommonParam param)
        {
            Dictionary<string, ACS_MODULE_ROLE> result = new Dictionary<string, ACS_MODULE_ROLE>();
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
