using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportType.Get.ListV
{
    class SarReportTypeGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarReportTypeGetListV
    {
        SarReportTypeViewFilterQuery filterQuery;

        internal SarReportTypeGetListVBehaviorByViewFilterQuery(CommonParam param, SarReportTypeViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_REPORT_TYPE> ISarReportTypeGetListV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeDAO.GetView(filterQuery.Query(), param);
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
