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
        internal HIS_EXME_REASON_CFG GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisExmeReasonCfgFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXME_REASON_CFG GetByCode(string code, HisExmeReasonCfgFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExmeReasonCfgDAO.GetByCode(code, filter.Query());
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
