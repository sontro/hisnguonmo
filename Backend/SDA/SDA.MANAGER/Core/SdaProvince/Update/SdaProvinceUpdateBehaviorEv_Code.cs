using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince.Update
{
    class SdaProvinceUpdateBehaviorEv : BeanObjectBase, ISdaProvinceUpdate
    {
        SDA_PROVINCE entity;

        internal SdaProvinceUpdateBehaviorEv(CommonParam param, SDA_PROVINCE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceDAO.Update(entity);
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
                result = result && SdaProvinceCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaProvinceCheckVerifyExistsCode.Verify(param, entity.PROVINCE_CODE, entity.ID);
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
