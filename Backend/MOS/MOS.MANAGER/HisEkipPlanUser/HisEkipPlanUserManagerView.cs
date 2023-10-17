using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    public partial class HisEkipPlanUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_EKIP_PLAN_USER>> GetView(HisEkipPlanUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EKIP_PLAN_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EKIP_PLAN_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipPlanUserGet(param).GetView(filter);
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
