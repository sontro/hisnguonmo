using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.ListV
{
    class SdaCustomizeUiGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaCustomizeUiGetListV
    {
        SdaCustomizeUiViewFilterQuery filterQuery;

        internal SdaCustomizeUiGetListVBehaviorByViewFilterQuery(CommonParam param, SdaCustomizeUiViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_CUSTOMIZE_UI> ISdaCustomizeUiGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaCustomizeUiDAO.GetView(filterQuery.Query(), param);
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
