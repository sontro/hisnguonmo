using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    public partial class HisBcsMetyReqDtManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BCS_METY_REQ_DT>> GetView(HisBcsMetyReqDtViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BCS_METY_REQ_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BCS_METY_REQ_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMetyReqDtGet(param).GetView(filter);
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
