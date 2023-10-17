using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    public partial class HisImpUserTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_USER_TEMP>> GetView(HisImpUserTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_USER_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_USER_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisImpUserTempGet(param).GetView(filter);
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
