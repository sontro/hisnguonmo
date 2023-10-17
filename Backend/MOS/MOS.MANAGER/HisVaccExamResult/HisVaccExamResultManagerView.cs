using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccExamResult
{
    public partial class HisVaccExamResultManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_VACC_EXAM_RESULT>> GetView(HisVaccExamResultViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_VACC_EXAM_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_VACC_EXAM_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccExamResultGet(param).GetView(filter);
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
