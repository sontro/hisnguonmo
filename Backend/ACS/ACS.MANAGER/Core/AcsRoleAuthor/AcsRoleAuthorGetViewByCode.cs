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
        internal V_ACS_ROLE_AUTHOR GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new AcsRoleAuthorViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_ACS_ROLE_AUTHOR GetViewByCode(string code, AcsRoleAuthorViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsRoleAuthorDAO.GetViewByCode(code, filter.Query());
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
