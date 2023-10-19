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
        internal AcsOtpGet()
            : base()
        {

        }

        internal AcsOtpGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_OTP> Get(AcsOtpFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsOtpDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_OTP GetById(long id)
        {
            try
            {
                return GetById(id, new AcsOtpFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS_OTP GetById(long id, AcsOtpFilterQuery filter)
        {
            try
            {
                return DAOWorker.AcsOtpDAO.GetById(id, filter.Query());
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
