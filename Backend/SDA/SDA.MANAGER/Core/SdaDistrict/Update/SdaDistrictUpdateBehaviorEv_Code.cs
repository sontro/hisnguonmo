using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict.Update
{
    class SdaDistrictUpdateBehaviorEv : BeanObjectBase, ISdaDistrictUpdate
    {
        SDA_DISTRICT entity;

        internal SdaDistrictUpdateBehaviorEv(CommonParam param, SDA_DISTRICT data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictDAO.Update(entity);
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
                result = result && SdaDistrictCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaDistrictCheckVerifyExistsCode.Verify(param, entity.DISTRICT_CODE, entity.ID);
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
