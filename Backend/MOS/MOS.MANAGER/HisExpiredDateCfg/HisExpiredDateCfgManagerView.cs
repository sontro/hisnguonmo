using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    public partial class HisExpiredDateCfgManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXPIRED_DATE_CFG>> GetView(HisExpiredDateCfgViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXPIRED_DATE_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXPIRED_DATE_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisExpiredDateCfgGet(param).GetView(filter);
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
