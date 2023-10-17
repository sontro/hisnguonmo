using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcd
{
    public partial class HisIcdManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ICD>> GetView(HisIcdViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ICD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ICD> resultData = null;
                if (valid)
                {
                    resultData = new HisIcdGet(param).GetView(filter);
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
