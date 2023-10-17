using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    public partial class HisRationTimeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_RATION_TIME>> GetView(HisRationTimeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_RATION_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_RATION_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisRationTimeGet(param).GetView(filter);
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
