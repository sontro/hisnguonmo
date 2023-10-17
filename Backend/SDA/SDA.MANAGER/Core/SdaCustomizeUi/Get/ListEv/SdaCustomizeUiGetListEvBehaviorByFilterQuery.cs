using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaCustomizeUi.Get.ListEv
{
    class SdaCustomizeUiGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaCustomizeUiGetListEv
    {
        SdaCustomizeUiFilterQuery filterQuery;

        internal SdaCustomizeUiGetListEvBehaviorByFilterQuery(CommonParam param, SdaCustomizeUiFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_CUSTOMIZE_UI> ISdaCustomizeUiGetListEv.Run()
        {
            try
            {
                return DAOWorker.SdaCustomizeUiDAO.Get(filterQuery.Query(), param);
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
