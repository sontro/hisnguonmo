using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLicense.Create
{
    class SdaLicenseCreateBehaviorListEv : BeanObjectBase, ISdaLicenseCreate
    {
        List<SDA_LICENSE> entities;

        internal SdaLicenseCreateBehaviorListEv(CommonParam param, List<SDA_LICENSE> datas)
            : base(param)
        {
            entities = datas;
        }

        object ISdaLicenseCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLicenseDAO.CreateList(entities);
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
                result = result && SdaLicenseCheckVerifyValidData.Verify(param, entities);
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
