using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType.Delete
{
    class AcsOtpTypeDeleteBehaviorEv : BeanObjectBase, IAcsOtpTypeDelete
    {
        ACS_OTP_TYPE entity;

        internal AcsOtpTypeDeleteBehaviorEv(CommonParam param, ACS_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsOtpTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsOtpTypeDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsOtpTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
