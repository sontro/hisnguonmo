using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqReq
{
    public partial class HisBcsMetyReqReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BCS_METY_REQ_REQ>> GetView(HisBcsMetyReqReqViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BCS_METY_REQ_REQ>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BCS_METY_REQ_REQ> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMetyReqReqGet(param).GetView(filter);
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
