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
        internal List<V_HIS_EXECUTE_ROLE> GetView(HisExecuteRoleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExecuteRoleDAO.GetView(filter.Query(), param);
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
