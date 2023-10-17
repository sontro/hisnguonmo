using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig.Update
{
    class SdaConfigUpdateBehaviorEv : BeanObjectBase, ISdaConfigUpdate
    {
        SDA_CONFIG entity;

        internal SdaConfigUpdateBehaviorEv(CommonParam param, SDA_CONFIG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigDAO.Update(entity);
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
                result = result && SdaConfigCheckVerifyValidData.Verify(param, entity);
                result = result && SdaConfigCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaConfigCheckVerifyExistsCode.Verify(param, entity.KEY, entity.ID);
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
