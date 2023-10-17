using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    public partial class HisExmeReasonCfgManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXME_REASON_CFG>> GetView(HisExmeReasonCfgViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXME_REASON_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXME_REASON_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisExmeReasonCfgGet(param).GetView(filter);
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
