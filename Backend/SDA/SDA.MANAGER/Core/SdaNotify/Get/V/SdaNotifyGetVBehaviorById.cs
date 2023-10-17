using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify.Get.V
{
    class SdaNotifyGetVBehaviorById : BeanObjectBase, ISdaNotifyGetV
    {
        long id;

        internal SdaNotifyGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_NOTIFY ISdaNotifyGetV.Run()
        {
            try
            {
                return DAOWorker.SdaNotifyDAO.GetViewById(id, new SdaNotifyViewFilterQuery().Query());
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
