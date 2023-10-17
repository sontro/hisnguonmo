using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarRetyFofi.Get.ListV
{
    class SarRetyFofiGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarRetyFofiGetListV
    {
        SarRetyFofiViewFilterQuery filterQuery;

        internal SarRetyFofiGetListVBehaviorByViewFilterQuery(CommonParam param, SarRetyFofiViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_RETY_FOFI> ISarRetyFofiGetListV.Run()
        {
            try
            {
                return DAOWorker.SarRetyFofiDAO.GetView(filterQuery.Query(), param);
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
