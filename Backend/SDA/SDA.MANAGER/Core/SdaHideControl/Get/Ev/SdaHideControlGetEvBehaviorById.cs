using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl.Get.Ev
{
    class SdaHideControlGetEvBehaviorById : BeanObjectBase, ISdaHideControlGetEv
    {
        long id;

        internal SdaHideControlGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_HIDE_CONTROL ISdaHideControlGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaHideControlDAO.GetById(id, new SdaHideControlFilterQuery().Query());
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
