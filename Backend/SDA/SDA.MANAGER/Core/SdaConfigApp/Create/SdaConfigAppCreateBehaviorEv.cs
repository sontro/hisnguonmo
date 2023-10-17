using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp.Create
{
    class SdaConfigAppCreateBehaviorEv : BeanObjectBase, ISdaConfigAppCreate
    {
        SDA_CONFIG_APP entity;

        internal SdaConfigAppCreateBehaviorEv(CommonParam param, SDA_CONFIG_APP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppDAO.Create(entity);
                if (result) { SdaConfigAppEventLogCreate.Log(entity); }
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
                result = result && SdaConfigAppCheckVerifyValidData.Verify(param, entity);
                result = result && SdaConfigAppCheckVerifyExistsCode.Verify(param, entity.KEY, null);
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
