using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytWhitelist
{
    partial class HisBhytWhitelistGet : BusinessBase
    {
        internal V_HIS_BHYT_WHITELIST GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBhytWhitelistViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BHYT_WHITELIST GetViewByCode(string code, HisBhytWhitelistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytWhitelistDAO.GetViewByCode(code, filter.Query());
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
