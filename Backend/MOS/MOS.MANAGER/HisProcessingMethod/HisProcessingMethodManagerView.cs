using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    public partial class HisProcessingMethodManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PROCESSING_METHOD>> GetView(HisProcessingMethodViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PROCESSING_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PROCESSING_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisProcessingMethodGet(param).GetView(filter);
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
