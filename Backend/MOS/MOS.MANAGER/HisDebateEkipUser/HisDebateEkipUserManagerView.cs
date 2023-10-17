using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    public partial class HisDebateEkipUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEBATE_EKIP_USER>> GetView(HisDebateEkipUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEBATE_EKIP_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEBATE_EKIP_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateEkipUserGet(param).GetView(filter);
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
