using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog.Get.V
{
    class SdaEventLogGetVBehaviorById : BeanObjectBase, ISdaEventLogGetV
    {
        long id;

        internal SdaEventLogGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_EVENT_LOG ISdaEventLogGetV.Run()
        {
            try
            {
                return DAOWorker.SdaEventLogDAO.GetViewById(id, new SdaEventLogViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
