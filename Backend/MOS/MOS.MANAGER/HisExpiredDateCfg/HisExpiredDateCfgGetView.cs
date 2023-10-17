using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    partial class HisExpiredDateCfgGet : BusinessBase
    {
        internal List<V_HIS_EXPIRED_DATE_CFG> GetView(HisExpiredDateCfgViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpiredDateCfgDAO.GetView(filter.Query(), param);
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
