using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigAppUser.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Delete
{
    class SdaConfigAppUserDeleteBehaviorEv : BeanObjectBase, ISdaConfigAppUserDelete
    {
        SDA_CONFIG_APP_USER entity;

        internal SdaConfigAppUserDeleteBehaviorEv(CommonParam param, SDA_CONFIG_APP_USER data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUserDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppUserDAO.Truncate(entity);
                if (result) { SdaConfigAppUserEventLogDelete.Log(entity); }
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
                result = result && SdaConfigAppUserCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
