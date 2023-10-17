using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Get.V
{
    class SdaHideControlGetVBehaviorById : BeanObjectBase, ISdaHideControlGetV
    {
        long id;

        internal SdaHideControlGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_HIDE_CONTROL ISdaHideControlGetV.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.GetViewById(id, new SdaHideControlViewFilterQuery().Query());
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
