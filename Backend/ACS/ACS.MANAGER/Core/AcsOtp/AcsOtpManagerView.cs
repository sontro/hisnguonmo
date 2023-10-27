using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsOtp
{
    public partial class AcsOtpManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<V_ACS_OTP>> GetView(AcsOtpViewFilterQuery filter)
        {
            ApiResultObject<List<V_ACS_OTP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_ACS_OTP> resultData = null;
                if (valid)
                {
                    resultData = new AcsOtpGet(param).GetView(filter);
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
