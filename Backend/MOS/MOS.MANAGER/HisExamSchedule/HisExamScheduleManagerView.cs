using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    public partial class HisExamScheduleManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EXAM_SCHEDULE>> GetView(HisExamScheduleViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXAM_SCHEDULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXAM_SCHEDULE> resultData = null;
                if (valid)
                {
                    resultData = new HisExamScheduleGet(param).GetView(filter);
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
