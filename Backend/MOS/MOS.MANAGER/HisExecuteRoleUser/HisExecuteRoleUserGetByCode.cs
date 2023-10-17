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
        internal HIS_EXECUTE_ROLE_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisExecuteRoleUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXECUTE_ROLE_USER GetByCode(string code, HisExecuteRoleUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExecuteRoleUserDAO.GetByCode(code, filter.Query());
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
