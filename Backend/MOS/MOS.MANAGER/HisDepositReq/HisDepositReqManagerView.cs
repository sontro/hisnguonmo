using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReq
{
    public partial class HisDepositReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEPOSIT_REQ>> GetView(HisDepositReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEPOSIT_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEPOSIT_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisDepositReqGet(param).GetView(filter);
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
