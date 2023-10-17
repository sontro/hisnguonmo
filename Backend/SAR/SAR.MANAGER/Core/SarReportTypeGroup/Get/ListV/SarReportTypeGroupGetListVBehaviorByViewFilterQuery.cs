using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.ListV
{
    class SarReportTypeGroupGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarReportTypeGroupGetListV
    {
        SarReportTypeGroupViewFilterQuery filterQuery;

        internal SarReportTypeGroupGetListVBehaviorByViewFilterQuery(CommonParam param, SarReportTypeGroupViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_REPORT_TYPE_GROUP> ISarReportTypeGroupGetListV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.GetView(filterQuery.Query(), param);
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
