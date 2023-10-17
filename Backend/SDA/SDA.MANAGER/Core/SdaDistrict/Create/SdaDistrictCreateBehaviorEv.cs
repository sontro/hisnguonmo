using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict.Create
{
    class SdaDistrictCreateBehaviorEv : BeanObjectBase, ISdaDistrictCreate
    {
        SDA_DISTRICT entity;

        internal SdaDistrictCreateBehaviorEv(CommonParam param, SDA_DISTRICT data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictDAO.Create(entity);
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
                result = result && SdaDistrictCheckVerifyValidData.Verify(param, entity);
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
