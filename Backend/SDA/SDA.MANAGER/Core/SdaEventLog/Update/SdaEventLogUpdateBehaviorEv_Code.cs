using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog.Update
{
    class SdaEventLogUpdateBehaviorEv : BeanObjectBase, ISdaEventLogUpdate
    {
        SDA_EVENT_LOG entity;

        internal SdaEventLogUpdateBehaviorEv(CommonParam param, SDA_EVENT_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEventLogUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEventLogDAO.Update(entity);
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
                result = result && SdaEventLogCheckVerifyValidData.Verify(param, entity);
                result = result && SdaEventLogCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SdaEventLogCheckVerifyExistsCode.Verify(param, entity.EVENT_LOG_CODE, entity.ID);
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
