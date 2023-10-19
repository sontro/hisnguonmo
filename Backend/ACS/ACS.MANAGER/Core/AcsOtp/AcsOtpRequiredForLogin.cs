using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.AcsOtp.OtpRequiredForLogin;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpRequiredForLogin : BusinessBase
    {
        internal AcsOtpRequiredForLogin()
            : base()
        {

        }

        internal AcsOtpRequiredForLogin(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal OtpRequiredForLoginResultSDO Required(OtpRequiredForLoginSDO data)
        {
            OtpRequiredForLoginResultSDO result = null;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    IAcsOtpOtpRequiredForLogin behavior = AcsOtpOtpRequiredForLoginBehaviorFactory.MakeIAcsOtpOtpRequiredForLogin(param, data);
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
