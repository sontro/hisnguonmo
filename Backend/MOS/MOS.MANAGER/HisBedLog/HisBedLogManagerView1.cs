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
        public ApiResultObject<List<V_HIS_BED_LOG_1>> GetView1(HisBedLogView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_LOG_1>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_LOG_1> resultData = null;
                if (valid)
                {
                    resultData = new HisBedLogGet(param).GetView1(filter);
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
