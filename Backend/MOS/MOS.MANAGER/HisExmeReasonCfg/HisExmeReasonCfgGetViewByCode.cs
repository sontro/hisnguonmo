using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    partial class HisExmeReasonCfgGet : BusinessBase
    {
        internal V_HIS_EXME_REASON_CFG GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExmeReasonCfgViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXME_REASON_CFG GetViewByCode(string code, HisExmeReasonCfgViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExmeReasonCfgDAO.GetViewByCode(code, filter.Query());
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
