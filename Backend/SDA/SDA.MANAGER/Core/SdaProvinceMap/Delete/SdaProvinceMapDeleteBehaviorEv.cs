using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap.Delete
{
    class SdaProvinceMapDeleteBehaviorEv : BeanObjectBase, ISdaProvinceMapDelete
    {
        SDA_PROVINCE_MAP entity;

        internal SdaProvinceMapDeleteBehaviorEv(CommonParam param, SDA_PROVINCE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceMapDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceMapDAO.Truncate(entity);
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
                result = result && SdaProvinceMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
