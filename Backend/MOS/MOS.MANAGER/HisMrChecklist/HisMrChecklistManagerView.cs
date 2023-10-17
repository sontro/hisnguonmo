using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    public partial class HisMrChecklistManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_MR_CHECKLIST>> GetView(HisMrChecklistViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MR_CHECKLIST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MR_CHECKLIST> resultData = null;
                if (valid)
                {
                    resultData = new HisMrChecklistGet(param).GetView(filter);
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
