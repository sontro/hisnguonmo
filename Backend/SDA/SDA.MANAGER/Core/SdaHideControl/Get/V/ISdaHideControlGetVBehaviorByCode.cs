using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Get.V
{
    class SdaHideControlGetVBehaviorByCode : BeanObjectBase, ISdaHideControlGetV
    {
        string code;

        internal SdaHideControlGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_HIDE_CONTROL ISdaHideControlGetV.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.GetViewByCode(code, new SdaHideControlViewFilterQuery().Query());
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
