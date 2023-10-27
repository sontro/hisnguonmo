using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsOtp
{
    partial class AcsOtpGet : BusinessBase
    {
        internal ACS_OTP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new AcsOtpFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_OTP GetByCode(string code, AcsOtpFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsOtpDAO.GetByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
