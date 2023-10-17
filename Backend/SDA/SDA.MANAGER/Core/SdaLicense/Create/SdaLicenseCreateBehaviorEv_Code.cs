using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Create
{
    class SdaLicenseCreateBehaviorEv : BeanObjectBase, ISdaLicenseCreate
    {
        SDA_LICENSE entity;

        internal SdaLicenseCreateBehaviorEv(CommonParam param, SDA_LICENSE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLicenseCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLicenseDAO.Create(entity);
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
                result = result && SdaLicenseCheckVerifyExistsCode.Verify(param, entity.LICENSE_CODE, null);
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
