using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgGet : BusinessBase
    {
        internal List<V_HIS_WARNING_FEE_CFG> GetView(HisWarningFeeCfgViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWarningFeeCfgDAO.GetView(filter.Query(), param);
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
