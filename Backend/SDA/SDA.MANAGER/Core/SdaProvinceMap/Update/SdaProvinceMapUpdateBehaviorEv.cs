using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap.Update
{
    class SdaProvinceMapUpdateBehaviorEv : BeanObjectBase, ISdaProvinceMapUpdate
    {
        SDA_PROVINCE_MAP current;
        SDA_PROVINCE_MAP entity;

        internal SdaProvinceMapUpdateBehaviorEv(CommonParam param, SDA_PROVINCE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceMapUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceMapDAO.Update(entity);
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
                result = result && SdaProvinceMapCheckVerifyValidData.Verify(param, entity);
                result = result && SdaProvinceMapCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
