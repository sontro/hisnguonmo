using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillFund
{
    public partial class HisBillFundManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BILL_FUND>> GetView(HisBillFundViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BILL_FUND>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BILL_FUND> resultData = null;
                if (valid)
                {
                    resultData = new HisBillFundGet(param).GetView(filter);
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
