using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransReq
{
    public partial class HisTransReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_TRANS_REQ>> GetView(HisTransReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANS_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANS_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisTransReqGet(param).GetView(filter);
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
