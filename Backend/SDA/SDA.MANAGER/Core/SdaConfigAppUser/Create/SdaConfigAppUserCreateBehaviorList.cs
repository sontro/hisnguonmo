using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaConfigAppUser.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Create
{
    class SdaConfigAppUserCreateBehaviorList : BeanObjectBase, ISdaConfigAppUserCreate
    {
        List<SDA_CONFIG_APP_USER> entity;

        internal SdaConfigAppUserCreateBehaviorList(CommonParam param, List<SDA_CONFIG_APP_USER> data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaConfigAppUserCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigAppUserDAO.CreateList(entity);
                if (result) { SdaConfigAppUserEventLogCreate.Log(entity); }
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
