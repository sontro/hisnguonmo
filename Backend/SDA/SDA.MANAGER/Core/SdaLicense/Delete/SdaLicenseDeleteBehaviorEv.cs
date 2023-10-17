using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLicense.Delete
{
    class SdaLicenseDeleteBehaviorEv : BeanObjectBase, ISdaLicenseDelete
    {
        SDA_LICENSE entity;

        internal SdaLicenseDeleteBehaviorEv(CommonParam param, SDA_LICENSE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLicenseDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLicenseDAO.Truncate(entity);
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
                result = result && SdaLicenseCheckVerifyIsUnlock.Verify(param, entity.ID);
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
