using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsRoleAuthor
{
    partial class AcsRoleAuthorGet : BusinessBase
    {
        internal ACS_ROLE_AUTHOR GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new AcsRoleAuthorFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_ROLE_AUTHOR GetByCode(string code, AcsRoleAuthorFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsRoleAuthorDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
