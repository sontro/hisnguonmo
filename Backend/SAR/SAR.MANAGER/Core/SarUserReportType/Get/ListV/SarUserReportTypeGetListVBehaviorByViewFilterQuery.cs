using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.Get.ListV
{
    class SarUserReportTypeGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarUserReportTypeGetListV
    {
        SarUserReportTypeViewFilterQuery filterQuery;

        internal SarUserReportTypeGetListVBehaviorByViewFilterQuery(CommonParam param, SarUserReportTypeViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_USER_REPORT_TYPE> ISarUserReportTypeGetListV.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.GetView(filterQuery.Query(), param);
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
