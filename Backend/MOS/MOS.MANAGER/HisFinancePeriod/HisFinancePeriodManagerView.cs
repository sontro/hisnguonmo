using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    public partial class HisFinancePeriodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_FINANCE_PERIOD>> GetView(HisFinancePeriodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_FINANCE_PERIOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_FINANCE_PERIOD> resultData = null;
                if (valid)
                {
                    resultData = new HisFinancePeriodGet(param).GetView(filter);
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
