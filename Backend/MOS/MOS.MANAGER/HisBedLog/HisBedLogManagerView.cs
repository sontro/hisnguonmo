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
        public ApiResultObject<List<V_HIS_BED_LOG>> GetView(HisBedLogViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BED_LOG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BED_LOG> resultData = null;
                if (valid)
                {
                    resultData = new HisBedLogGet(param).GetView(filter);
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
