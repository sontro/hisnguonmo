using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    public partial class HisDebateInviteUserManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_HIS_DEBATE_INVITE_USER>> GetView(HisDebateInviteUserViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DEBATE_INVITE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DEBATE_INVITE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateInviteUserGet(param).GetView(filter);
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
