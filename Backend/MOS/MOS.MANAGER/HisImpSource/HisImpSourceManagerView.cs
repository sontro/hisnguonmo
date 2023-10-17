using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpSource
{
    public partial class HisImpSourceManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_IMP_SOURCE>> GetView(HisImpSourceViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_IMP_SOURCE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_IMP_SOURCE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpSourceGet(param).GetView(filter);
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
