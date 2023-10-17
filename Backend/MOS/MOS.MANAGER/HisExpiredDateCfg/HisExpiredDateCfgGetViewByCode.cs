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
        internal V_HIS_EXPIRED_DATE_CFG GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExpiredDateCfgViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXPIRED_DATE_CFG GetViewByCode(string code, HisExpiredDateCfgViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpiredDateCfgDAO.GetViewByCode(code, filter.Query());
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
