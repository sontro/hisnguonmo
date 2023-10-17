using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytWhitelist
{
    public partial class HisBhytWhitelistManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BHYT_WHITELIST>> GetView(HisBhytWhitelistViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BHYT_WHITELIST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BHYT_WHITELIST> resultData = null;
                if (valid)
                {
                    resultData = new HisBhytWhitelistGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
