using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using ACS.SDO;
using ACS.MANAGER.Core.AcsOtp.OtpRequiredOnly;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpRequiredOnly : BusinessBase
    {
        internal AcsOtpRequiredOnly()
            : base()
        {

        }

        internal AcsOtpRequiredOnly(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool RequiredOnly(OtpRequiredOnlySDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsOtpCheck checker = new AcsOtpCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    IAcsOtpOtpRequiredOnly behavior = AcsOtpOtpRequiredOnlyBehaviorFactory.MakeIAcsOtpOtpRequiredOnly(param, data);
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
