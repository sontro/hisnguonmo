using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRegisterReq
{
    public partial class HisRegisterReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_REGISTER_REQ>> GetView(HisRegisterReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_REGISTER_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_REGISTER_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisRegisterReqGet(param).GetView(filter);
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
