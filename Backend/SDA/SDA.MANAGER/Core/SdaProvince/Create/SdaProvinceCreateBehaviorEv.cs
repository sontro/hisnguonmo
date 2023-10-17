using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince.Create
{
    class SdaProvinceCreateBehaviorEv : BeanObjectBase, ISdaProvinceCreate
    {
        SDA_PROVINCE entity;

        internal SdaProvinceCreateBehaviorEv(CommonParam param, SDA_PROVINCE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceDAO.Create(entity);
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
                result = result && SdaProvinceCheckVerifyValidData.Verify(param, entity);
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
