using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamServiceTemp
{
    public partial class HisExamServiceTempManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXAM_SERVICE_TEMP>> GetView(HisExamServiceTempViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXAM_SERVICE_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXAM_SERVICE_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisExamServiceTempGet(param).GetView(filter);
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
