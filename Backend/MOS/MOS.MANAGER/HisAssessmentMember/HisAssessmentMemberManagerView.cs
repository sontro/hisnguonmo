using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentMember
{
    public partial class HisAssessmentMemberManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_ASSESSMENT_MEMBER>> GetView(HisAssessmentMemberViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_ASSESSMENT_MEMBER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_ASSESSMENT_MEMBER> resultData = null;
                if (valid)
                {
                    resultData = new HisAssessmentMemberGet(param).GetView(filter);
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
