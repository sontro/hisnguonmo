using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebate
{
    public partial class HisDebateManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEBATE>> GetView(HisDebateViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEBATE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEBATE> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateGet(param).GetView(filter);
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
