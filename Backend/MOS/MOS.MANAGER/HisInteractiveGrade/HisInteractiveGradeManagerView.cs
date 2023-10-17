using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    public partial class HisInteractiveGradeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_INTERACTIVE_GRADE>> GetView(HisInteractiveGradeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_INTERACTIVE_GRADE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_INTERACTIVE_GRADE> resultData = null;
                if (valid)
                {
                    resultData = new HisInteractiveGradeGet(param).GetView(filter);
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
