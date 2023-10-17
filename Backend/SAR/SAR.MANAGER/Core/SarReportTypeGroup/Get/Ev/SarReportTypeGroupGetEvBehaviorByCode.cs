using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.Ev
{
    class SarReportTypeGroupGetEvBehaviorByCode : BeanObjectBase, ISarReportTypeGroupGetEv
    {
        string code;

        internal SarReportTypeGroupGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_REPORT_TYPE_GROUP ISarReportTypeGroupGetEv.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.GetByCode(code, new SarReportTypeGroupFilterQuery().Query());
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
