using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    public partial class HisBedLogManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BED_LOG_4>> GetView4(HisBedLogView4FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_LOG_4>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_LOG_4> resultData = null;
                if (valid)
                {
                    resultData = new HisBedLogGet(param).GetView4(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
