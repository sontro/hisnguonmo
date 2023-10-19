using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.AcsOtp.OtpRequiredWithMessage;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpRequiredWithMessage : BusinessBase
    {
        internal AcsOtpRequiredWithMessage()
            : base()
        {

        }

        internal AcsOtpRequiredWithMessage(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Required(OtpRequiredWithMessageSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    IAcsOtpOtpRequiredWithMessage behavior = AcsOtpOtpRequiredWithMessageBehaviorFactory.MakeIAcsOtpOtpRequiredWithMessage(param, data);
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
