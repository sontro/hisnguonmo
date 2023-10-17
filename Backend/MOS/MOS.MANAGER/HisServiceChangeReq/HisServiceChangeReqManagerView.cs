using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceChangeReq
{
    public partial class HisServiceChangeReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_CHANGE_REQ>> GetView(HisServiceChangeReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_CHANGE_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_CHANGE_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceChangeReqGet(param).GetView(filter);
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
