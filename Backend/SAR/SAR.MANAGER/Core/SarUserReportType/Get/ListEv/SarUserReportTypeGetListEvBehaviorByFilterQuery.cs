using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarUserReportType.Get.ListEv
{
    class SarUserReportTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarUserReportTypeGetListEv
    {
        SarUserReportTypeFilterQuery filterQuery;

        internal SarUserReportTypeGetListEvBehaviorByFilterQuery(CommonParam param, SarUserReportTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_USER_REPORT_TYPE> ISarUserReportTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.Get(filterQuery.Query(), param);
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
