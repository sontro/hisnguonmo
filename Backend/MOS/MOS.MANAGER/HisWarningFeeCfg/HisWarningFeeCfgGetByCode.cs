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
        internal HIS_WARNING_FEE_CFG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisWarningFeeCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WARNING_FEE_CFG GetByCode(string code, HisWarningFeeCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWarningFeeCfgDAO.GetByCode(code, filter.Query());
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
