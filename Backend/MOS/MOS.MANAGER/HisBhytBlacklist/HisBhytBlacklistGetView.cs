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
        internal List<V_HIS_BHYT_BLACKLIST> GetView(HisBhytBlacklistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytBlacklistDAO.GetView(filter.Query(), param);
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
