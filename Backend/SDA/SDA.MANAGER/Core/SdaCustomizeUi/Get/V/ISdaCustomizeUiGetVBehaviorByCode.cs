using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.V
{
    class SdaCustomizeUiGetVBehaviorByCode : BeanObjectBase, ISdaCustomizeUiGetV
    {
        string code;

        internal SdaCustomizeUiGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_CUSTOMIZE_UI ISdaCustomizeUiGetV.Run()
        {
            try
            {
                return DAOWorker.SdaCustomizeUiDAO.GetViewByCode(code, new SdaCustomizeUiViewFilterQuery().Query());
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
