using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    public partial class HisBhytParamManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BHYT_PARAM>> GetView(HisBhytParamViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BHYT_PARAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BHYT_PARAM> resultData = null;
                if (valid)
                {
                    resultData = new HisBhytParamGet(param).GetView(filter);
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
