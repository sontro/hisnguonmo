using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    public partial class HisBranchTimeManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_BRANCH_TIME>> GetView(HisBranchTimeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_BRANCH_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_BRANCH_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisBranchTimeGet(param).GetView(filter);
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
