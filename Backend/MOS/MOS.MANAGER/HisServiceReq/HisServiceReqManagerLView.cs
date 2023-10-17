using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<L_HIS_SERVICE_REQ>> GetLView(HisServiceReqLViewFilterQuery filter)
        {
            ApiResultObject<List<L_HIS_SERVICE_REQ>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<L_HIS_SERVICE_REQ> resultData = null;
                if (valid)
                {
                    filter.IS_RESTRICTED_KSK = HisKskContractCFG.RESTRICTED_ACCESSING;
                    resultData = new HisServiceReqGet(param).GetLView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}
