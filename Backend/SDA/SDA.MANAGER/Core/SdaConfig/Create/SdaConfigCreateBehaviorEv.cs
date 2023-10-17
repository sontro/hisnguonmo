using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig.Create
{
    class SdaConfigCreateBehaviorEv : BeanObjectBase, ISdaConfigCreate
    {
        SDA_CONFIG entity;

        internal SdaConfigCreateBehaviorEv(CommonParam param, SDA_CONFIG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigDAO.Create(entity);
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
