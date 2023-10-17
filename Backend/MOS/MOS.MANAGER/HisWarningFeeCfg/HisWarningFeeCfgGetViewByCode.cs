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
        internal V_HIS_WARNING_FEE_CFG GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisWarningFeeCfgViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_WARNING_FEE_CFG GetViewByCode(string code, HisWarningFeeCfgViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWarningFeeCfgDAO.GetViewByCode(code, filter.Query());
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
