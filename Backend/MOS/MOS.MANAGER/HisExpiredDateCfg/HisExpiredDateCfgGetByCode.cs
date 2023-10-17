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
        internal HIS_EXPIRED_DATE_CFG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisExpiredDateCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXPIRED_DATE_CFG GetByCode(string code, HisExpiredDateCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpiredDateCfgDAO.GetByCode(code, filter.Query());
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
