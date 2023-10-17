using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigAppUser.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Update
{
    class SdaConfigAppUserUpdateBehaviorEv : BeanObjectBase, ISdaConfigAppUserUpdate
    {
        SDA_CONFIG_APP_USER current;
        SDA_CONFIG_APP_USER entity;

        internal SdaConfigAppUserUpdateBehaviorEv(CommonParam param, SDA_CONFIG_APP_USER data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUserUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppUserDAO.Update(entity);
                if (result) { SdaConfigAppUserEventLogUpdate.Log(current, entity); }
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
                result = result && SdaConfigAppUserCheckVerifyValidData.Verify(param, entity);
                result = result && SdaConfigAppUserCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
