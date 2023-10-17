using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaNotify.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Create
{
    class SdaNotifyCreateBehaviorEv : BeanObjectBase, ISdaNotifyCreate
    {
        SDA_NOTIFY entity;

        internal SdaNotifyCreateBehaviorEv(CommonParam param, SDA_NOTIFY data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNotifyCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNotifyDAO.Create(entity);
                if (result) { SdaNotifyEventLogCreate.Log(entity); }
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
                result = result && SdaNotifyCheckVerifyValidData.Verify(param, entity);
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
