using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq.Exam;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReq.Update.Status;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {

        [Logger]
        public ApiResultObject<List<HisServiceReqDTO>> GetDynamic(HisServiceReqFilterQuery filter)
        {
            ApiResultObject<List<HisServiceReqDTO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisServiceReqDTO> resultData = null;
                if (valid)
                {
                    filter.IS_RESTRICTED_KSK = HisKskContractCFG.RESTRICTED_ACCESSING;
                    resultData = new HisServiceReqGet(param).GetDynamic(filter);
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
