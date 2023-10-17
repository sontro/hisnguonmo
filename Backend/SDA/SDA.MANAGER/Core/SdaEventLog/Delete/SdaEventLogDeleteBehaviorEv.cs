using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog.Delete
{
    class SdaEventLogDeleteBehaviorEv : BeanObjectBase, ISdaEventLogDelete
    {
        SDA_EVENT_LOG entity;

        internal SdaEventLogDeleteBehaviorEv(CommonParam param, SDA_EVENT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEventLogDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEventLogDAO.Truncate(entity);
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
                result = result && SdaEventLogCheckVerifyIsUnlock.Verify(param, entity.ID);
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
