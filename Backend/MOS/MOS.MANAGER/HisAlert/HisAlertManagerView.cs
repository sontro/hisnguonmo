using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAlert
{
    public partial class HisAlertManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ALERT>> GetView(HisAlertViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ALERT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ALERT> resultData = null;
                if (valid)
                {
                    resultData = new HisAlertGet(param).GetView(filter);
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
