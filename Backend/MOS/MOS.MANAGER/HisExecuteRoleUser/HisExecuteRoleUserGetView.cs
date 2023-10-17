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
        internal List<V_HIS_EXECUTE_ROLE_USER> GetView(HisExecuteRoleUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExecuteRoleUserDAO.GetView(filter.Query(), param);
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
