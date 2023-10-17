using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    public partial class HisMestPeriodBltyManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEST_PERIOD_BLTY>> GetView(HisMestPeriodBltyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PERIOD_BLTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PERIOD_BLTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodBltyGet(param).GetView(filter);
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
