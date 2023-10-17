using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    public partial class HisAssessmentObjectManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ASSESSMENT_OBJECT>> GetView(HisAssessmentObjectViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ASSESSMENT_OBJECT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ASSESSMENT_OBJECT> resultData = null;
                if (valid)
                {
                    resultData = new HisAssessmentObjectGet(param).GetView(filter);
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
