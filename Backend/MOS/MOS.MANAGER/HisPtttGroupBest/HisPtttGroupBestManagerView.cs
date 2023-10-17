using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    public partial class HisPtttGroupBestManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_PTTT_GROUP_BEST>> GetView(HisPtttGroupBestViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PTTT_GROUP_BEST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PTTT_GROUP_BEST> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttGroupBestGet(param).GetView(filter);
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
