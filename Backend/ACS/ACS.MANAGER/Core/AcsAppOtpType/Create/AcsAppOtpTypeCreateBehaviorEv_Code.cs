using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsAppOtpType.Create
{
    class AcsAppOtpTypeCreateBehaviorEv : BeanObjectBase, IAcsAppOtpTypeCreate
    {
        ACS_APP_OTP_TYPE entity;

        internal AcsAppOtpTypeCreateBehaviorEv(CommonParam param, ACS_APP_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsAppOtpTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsAppOtpTypeDAO.Create(entity);
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
                //result = result && AcsAppOtpTypeCheckVerifyExistsCode.Verify(param, entity.ACTIVITY_TYPE_CODE, null);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
