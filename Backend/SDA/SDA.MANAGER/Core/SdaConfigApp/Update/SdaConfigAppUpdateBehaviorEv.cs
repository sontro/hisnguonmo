using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp.Update
{
    class SdaConfigAppUpdateBehaviorEv : BeanObjectBase, ISdaConfigAppUpdate
    {
        SDA_CONFIG_APP current;
        SDA_CONFIG_APP entity;

        internal SdaConfigAppUpdateBehaviorEv(CommonParam param, SDA_CONFIG_APP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppDAO.Update(entity);
                if (result) { SdaConfigAppEventLogUpdate.Log(current, entity); }
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
                result = result && SdaConfigAppCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaConfigAppCheckVerifyExistsCode.Verify(param, entity.KEY, entity.ID);
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
