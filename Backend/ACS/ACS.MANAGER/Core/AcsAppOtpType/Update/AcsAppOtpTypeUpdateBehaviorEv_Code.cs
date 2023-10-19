using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType.Update
{
    class AcsAppOtpTypeUpdateBehaviorEv : BeanObjectBase, IAcsAppOtpTypeUpdate
    {
        ACS_APP_OTP_TYPE current;
        ACS_APP_OTP_TYPE entity;

        internal AcsAppOtpTypeUpdateBehaviorEv(CommonParam param, ACS_APP_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsAppOtpTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsAppOtpTypeDAO.Update(entity);
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
                result = result && AcsAppOtpTypeCheckVerifyValidData.Verify(param, entity);
                result = result && AcsAppOtpTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                //result = result && AcsAppOtpTypeCheckVerifyExistsCode.Verify(param, entity.ACTIVITY_TYPE_CODE, entity.ID);
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
