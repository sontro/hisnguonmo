using Inventec.Common.Logging;
using Inventec.Core;
using MOS.DynamicDTO;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
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
        public ApiResultObject<List<HisServiceReqViewDTO>> GetViewDynamic(HisServiceReqViewFilterQuery filter)
        {
            ApiResultObject<List<HisServiceReqViewDTO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisServiceReqViewDTO> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceReqGet(param).GetViewDynamic(filter);
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
