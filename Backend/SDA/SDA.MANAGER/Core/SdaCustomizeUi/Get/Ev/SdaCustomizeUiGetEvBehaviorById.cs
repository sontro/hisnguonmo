using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.Ev
{
    class SdaCustomizeUiGetEvBehaviorById : BeanObjectBase, ISdaCustomizeUiGetEv
    {
        long id;

        internal SdaCustomizeUiGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_CUSTOMIZE_UI ISdaCustomizeUiGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaCustomizeUiDAO.GetById(id, new SdaCustomizeUiFilterQuery().Query());
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
