using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.Ev
{
    class SarReportTypeGroupGetEvBehaviorById : BeanObjectBase, ISarReportTypeGroupGetEv
    {
        long id;

        internal SarReportTypeGroupGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_REPORT_TYPE_GROUP ISarReportTypeGroupGetEv.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.GetById(id, new SarReportTypeGroupFilterQuery().Query());
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
