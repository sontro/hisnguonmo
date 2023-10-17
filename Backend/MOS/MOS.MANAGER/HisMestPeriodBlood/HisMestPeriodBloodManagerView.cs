using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    public partial class HisMestPeriodBloodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_PERIOD_BLOOD>> GetView(HisMestPeriodBloodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PERIOD_BLOOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PERIOD_BLOOD> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodBloodGet(param).GetView(filter);
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
