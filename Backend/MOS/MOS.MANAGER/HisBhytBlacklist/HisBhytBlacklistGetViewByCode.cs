using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytBlacklist
{
    partial class HisBhytBlacklistGet : BusinessBase
    {
        internal V_HIS_BHYT_BLACKLIST GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBhytBlacklistViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BHYT_BLACKLIST GetViewByCode(string code, HisBhytBlacklistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytBlacklistDAO.GetViewByCode(code, filter.Query());
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
