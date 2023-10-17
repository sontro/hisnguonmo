using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRoleUser
{
    partial class HisExecuteRoleUserGet : BusinessBase
    {
        internal V_HIS_EXECUTE_ROLE_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExecuteRoleUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXECUTE_ROLE_USER GetViewByCode(string code, HisExecuteRoleUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExecuteRoleUserDAO.GetViewByCode(code, filter.Query());
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
