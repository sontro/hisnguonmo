using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceFollow
{
    public partial class HisServiceFollowManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_SERVICE_FOLLOW>> GetView(HisServiceFollowViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_SERVICE_FOLLOW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_SERVICE_FOLLOW> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceFollowGet(param).GetView(filter);
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
