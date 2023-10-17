using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog.Create
{
    class SdaEventLogCreateBehaviorListEv : BeanObjectBase, ISdaEventLogCreate
    {
        List<SDA_EVENT_LOG> entities;

        internal SdaEventLogCreateBehaviorListEv(CommonParam param, List<SDA_EVENT_LOG> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaEventLogCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEventLogDAO.CreateList(entities);
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
                result = result && SdaEventLogCheckVerifyValidData.Verify(param, entities);
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
