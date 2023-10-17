using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    public partial class HisBcsMatyReqReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BCS_MATY_REQ_REQ>> GetView(HisBcsMatyReqReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BCS_MATY_REQ_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BCS_MATY_REQ_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMatyReqReqGet(param).GetView(filter);
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
