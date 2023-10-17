using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog.Get.V
{
    class SdaEventLogGetVBehaviorByCode : BeanObjectBase, ISdaEventLogGetV
    {
        string code;

        internal SdaEventLogGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_EVENT_LOG ISdaEventLogGetV.Run()
        {
            try
            {
                return DAOWorker.SdaEventLogDAO.GetViewByCode(code, new SdaEventLogViewFilterQuery().Query());
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
