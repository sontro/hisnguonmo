using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    public partial class HisDebateReasonManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEBATE_REASON>> GetView(HisDebateReasonViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEBATE_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEBATE_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateReasonGet(param).GetView(filter);
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
