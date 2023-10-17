using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap.Create
{
    class SdaProvinceMapCreateBehaviorEv : BeanObjectBase, ISdaProvinceMapCreate
    {
        SDA_PROVINCE_MAP entity;

        internal SdaProvinceMapCreateBehaviorEv(CommonParam param, SDA_PROVINCE_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaProvinceMapCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaProvinceMapDAO.Create(entity);
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
