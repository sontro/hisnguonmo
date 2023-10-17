using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    public partial class HisSeseTransReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SESE_TRANS_REQ>> GetView(HisSeseTransReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SESE_TRANS_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SESE_TRANS_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisSeseTransReqGet(param).GetView(filter);
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
