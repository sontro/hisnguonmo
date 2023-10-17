using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince.Delete
{
    class SdaProvinceDeleteBehaviorEv : BeanObjectBase, ISdaProvinceDelete
    {
        SDA_PROVINCE entity;

        internal SdaProvinceDeleteBehaviorEv(CommonParam param, SDA_PROVINCE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceDAO.Truncate(entity);
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
                result = result && SdaProvinceCheckVerifyIsUnlock.Verify(param, entity.ID);
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
