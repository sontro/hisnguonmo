using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict.Delete
{
    class SdaDistrictDeleteBehaviorEv : BeanObjectBase, ISdaDistrictDelete
    {
        SDA_DISTRICT entity;

        internal SdaDistrictDeleteBehaviorEv(CommonParam param, SDA_DISTRICT data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDistrictDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDistrictDAO.Truncate(entity);
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
                result = result && SdaDistrictCheckVerifyIsUnlock.Verify(param, entity.ID);
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
