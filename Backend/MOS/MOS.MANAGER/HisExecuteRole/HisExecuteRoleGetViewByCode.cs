using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteRole
{
    partial class HisExecuteRoleGet : BusinessBase
    {
        internal V_HIS_EXECUTE_ROLE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExecuteRoleViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXECUTE_ROLE GetViewByCode(string code, HisExecuteRoleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExecuteRoleDAO.GetViewByCode(code, filter.Query());
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
