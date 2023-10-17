using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaNotify.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Delete
{
    class SdaNotifyDeleteBehaviorEv : BeanObjectBase, ISdaNotifyDelete
    {
        SDA_NOTIFY entity;

        internal SdaNotifyDeleteBehaviorEv(CommonParam param, SDA_NOTIFY data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNotifyDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNotifyDAO.Truncate(entity);
                if (result) { SdaNotifyEventLogDelete.Log(entity); }
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
                result = result && SdaNotifyCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
