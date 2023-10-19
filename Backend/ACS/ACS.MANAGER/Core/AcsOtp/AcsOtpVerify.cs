using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.AcsOtp.OtpRequiredOnly;
using ACS.MANAGER.Core.AcsOtp.OtpVerify;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpVerify : BusinessBase
    {
        internal AcsOtpVerify()
            : base()
        {

        }

        internal AcsOtpVerify(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Verify(OtpVerifySDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    IAcsOtpVerify behavior = AcsOtpVerifyBehaviorFactory.MakeIAcsOtpVerify(param, data);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
