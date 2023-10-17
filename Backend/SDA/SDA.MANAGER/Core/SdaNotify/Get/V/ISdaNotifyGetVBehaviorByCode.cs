using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Get.V
{
    class SdaNotifyGetVBehaviorByCode : BeanObjectBase, ISdaNotifyGetV
    {
        string code;

        internal SdaNotifyGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_NOTIFY ISdaNotifyGetV.Run()
        {
            try
            {
                return DAOWorker.SdaNotifyDAO.GetViewByCode(code, new SdaNotifyViewFilterQuery().Query());
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
