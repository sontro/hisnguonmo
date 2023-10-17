using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap.Create
{
    class SdaDistrictMapCreateBehaviorEv : BeanObjectBase, ISdaDistrictMapCreate
    {
        SDA_DISTRICT_MAP entity;

        internal SdaDistrictMapCreateBehaviorEv(CommonParam param, SDA_DISTRICT_MAP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictMapCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictMapDAO.Create(entity);
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
                result = result && SdaDistrictMapCheckVerifyValidData.Verify(param, entity);
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
