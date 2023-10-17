using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.V
{
    class SarReportTypeGroupGetVBehaviorByCode : BeanObjectBase, ISarReportTypeGroupGetV
    {
        string code;

        internal SarReportTypeGroupGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_REPORT_TYPE_GROUP ISarReportTypeGroupGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.GetViewByCode(code, new SarReportTypeGroupViewFilterQuery().Query());
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
