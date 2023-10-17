using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisServiceReqGroupByDateSDO>> GetGroupByDate(long treatmentId)
        {
            ApiResultObject<List<HisServiceReqGroupByDateSDO>> result = null;

            try
            {
                List<HisServiceReqGroupByDateSDO> resultData = null;
                resultData = new HisServiceReqGet(param).GetGroupByDate(treatmentId);
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
