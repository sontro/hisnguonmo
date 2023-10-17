using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    public partial class HisSeseDepoRepayManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SESE_DEPO_REPAY>> GetView(HisSeseDepoRepayViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SESE_DEPO_REPAY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SESE_DEPO_REPAY> resultData = null;
                if (valid)
                {
                    resultData = new HisSeseDepoRepayGet(param).GetView(filter);
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
