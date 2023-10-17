using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRejectAlert
{
    public partial class HisRejectAlertManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_REJECT_ALERT>> GetView(HisRejectAlertViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REJECT_ALERT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REJECT_ALERT> resultData = null;
                if (valid)
                {
                    resultData = new HisRejectAlertGet(param).GetView(filter);
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
