using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigApp.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp.Delete
{
    class SdaConfigAppDeleteBehaviorEv : BeanObjectBase, ISdaConfigAppDelete
    {
        SDA_CONFIG_APP entity;

        internal SdaConfigAppDeleteBehaviorEv(CommonParam param, SDA_CONFIG_APP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppDAO.Truncate(entity);
                if (result) { SdaConfigAppEventLogDelete.Log(entity); }
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
                result = result && SdaConfigAppCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
