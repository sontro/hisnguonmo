using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.ListEv
{
    class SarReportTypeGroupGetListEvBehaviorByFilterQuery : BeanObjectBase, ISarReportTypeGroupGetListEv
    {
        SarReportTypeGroupFilterQuery filterQuery;

        internal SarReportTypeGroupGetListEvBehaviorByFilterQuery(CommonParam param, SarReportTypeGroupFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SAR_REPORT_TYPE_GROUP> ISarReportTypeGroupGetListEv.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.Get(filterQuery.Query(), param);
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
