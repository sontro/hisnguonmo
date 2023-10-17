using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportStt.Get.ListV
{
    class SarReportSttGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISarReportSttGetListV
    {
        SarReportSttViewFilterQuery filterQuery;

        internal SarReportSttGetListVBehaviorByViewFilterQuery(CommonParam param, SarReportSttViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SAR_REPORT_STT> ISarReportSttGetListV.Run()
        {
            try
            {
                return DAOWorker.SarReportSttDAO.GetView(filterQuery.Query(), param);
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
