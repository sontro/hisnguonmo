using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.AcsOtp.OtpRequiredOnly;
using ACS.MANAGER.Core.AcsOtp.OtpVerifyForLogin;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpVerifyForLogin : BusinessBase
    {
        internal AcsOtpVerifyForLogin()
            : base()
        {

        }

        internal AcsOtpVerifyForLogin(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal Inventec.Token.Core.TokenData Verify(OtpVerifyForLoginSDO data)
        {
            Inventec.Token.Core.TokenData result = null;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    IAcsOtpVerifyForLogin behavior = AcsOtpVerifyForLoginBehaviorFactory.MakeIAcsOtpVerifyForLogin(param, data);
                    result = behavior != null ? behavior.Run() : null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
