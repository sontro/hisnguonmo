using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    public partial class HisImpUserTempDtManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_USER_TEMP_DT>> GetView(HisImpUserTempDtViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_USER_TEMP_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_USER_TEMP_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisImpUserTempDtGet(param).GetView(filter);
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
