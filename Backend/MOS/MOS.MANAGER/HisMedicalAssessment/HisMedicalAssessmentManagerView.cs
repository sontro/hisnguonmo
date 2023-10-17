using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    public partial class HisMedicalAssessmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MEDICAL_ASSESSMENT>> GetView(HisMedicalAssessmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEDICAL_ASSESSMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEDICAL_ASSESSMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicalAssessmentGet(param).GetView(filter);
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
