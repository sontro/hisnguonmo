using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaNotify.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Update
{
    class SdaNotifyUpdateBehaviorEv : BeanObjectBase, ISdaNotifyUpdate
    {
        SDA_NOTIFY current;
        SDA_NOTIFY entity;

        internal SdaNotifyUpdateBehaviorEv(CommonParam param, SDA_NOTIFY data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaNotifyUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaNotifyDAO.Update(entity);
                if (result) { SdaNotifyEventLogUpdate.Log(current, entity); }
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
                result = result && SdaNotifyCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaNotifyCheckVerifyExistsCode.Verify(param, entity.NOTIFY_CODE, entity.ID);
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
