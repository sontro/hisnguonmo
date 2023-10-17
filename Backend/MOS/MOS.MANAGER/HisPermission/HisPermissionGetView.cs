using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    partial class HisPermissionGet : BusinessBase
    {
        internal List<V_HIS_PERMISSION> GetView(HisPermissionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisPermissionDAO.GetView(filter.Query(), param);
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
