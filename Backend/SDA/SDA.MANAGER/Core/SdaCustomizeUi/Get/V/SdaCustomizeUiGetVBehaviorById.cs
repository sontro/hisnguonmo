using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.V
{
    class SdaCustomizeUiGetVBehaviorById : BeanObjectBase, ISdaCustomizeUiGetV
    {
        long id;

        internal SdaCustomizeUiGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_CUSTOMIZE_UI ISdaCustomizeUiGetV.Run()
        {
            try
            {
                return DAOWorker.SdaCustomizeUiDAO.GetViewById(id, new SdaCustomizeUiViewFilterQuery().Query());
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
