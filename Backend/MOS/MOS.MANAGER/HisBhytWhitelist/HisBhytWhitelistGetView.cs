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
        internal List<V_HIS_BHYT_WHITELIST> GetView(HisBhytWhitelistViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBhytWhitelistDAO.GetView(filter.Query(), param);
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
