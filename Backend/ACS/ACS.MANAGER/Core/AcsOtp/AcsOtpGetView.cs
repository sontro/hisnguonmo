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
        internal List<V_ACS_OTP> GetView(AcsOtpViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsOtpDAO.GetView(filter.Query(), param);
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
