using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsOtpType.Update
{
    class AcsOtpTypeUpdateBehaviorEv : BeanObjectBase, IAcsOtpTypeUpdate
    {
        ACS_OTP_TYPE current;
        ACS_OTP_TYPE entity;

        internal AcsOtpTypeUpdateBehaviorEv(CommonParam param, ACS_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsOtpTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsOtpTypeDAO.Update(entity);
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
                result = result && AcsOtpTypeCheckVerifyValidData.Verify(param, entity);
                result = result && AcsOtpTypeCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && AcsOtpTypeCheckVerifyExistsCode.Verify(param, entity.OPT_TYPE_CODE, entity.ID);
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
