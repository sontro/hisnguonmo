using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Update
{
    class SdaLicenseUpdateBehaviorEv : BeanObjectBase, ISdaLicenseUpdate
    {
        SDA_LICENSE entity;

        internal SdaLicenseUpdateBehaviorEv(CommonParam param, SDA_LICENSE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLicenseUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLicenseDAO.Update(entity);
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
                result = result && SdaLicenseCheckVerifyValidData.Verify(param, entity);
                result = result && SdaLicenseCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaLicenseCheckVerifyExistsCode.Verify(param, entity.LICENSE_CODE, entity.ID);
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
