using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Get.Ev
{
    class SdaHideControlGetEvBehaviorByCode : BeanObjectBase, ISdaHideControlGetEv
    {
        string code;

        internal SdaHideControlGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_HIDE_CONTROL ISdaHideControlGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.GetByCode(code, new SdaHideControlFilterQuery().Query());
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
