using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig.Delete
{
    class SdaConfigDeleteBehaviorEv : BeanObjectBase, ISdaConfigDelete
    {
        SDA_CONFIG entity;

        internal SdaConfigDeleteBehaviorEv(CommonParam param, SDA_CONFIG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigDAO.Truncate(entity);
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
                result = result && SdaConfigCheckVerifyIsUnlock.Verify(param, entity.ID);
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
