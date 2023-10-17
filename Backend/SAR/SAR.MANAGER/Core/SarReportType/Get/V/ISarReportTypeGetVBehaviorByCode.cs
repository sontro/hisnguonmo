using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType.Get.V
{
    class SarReportTypeGetVBehaviorByCode : BeanObjectBase, ISarReportTypeGetV
    {
        string code;

        internal SarReportTypeGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_REPORT_TYPE ISarReportTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeDAO.GetViewByCode(code, new SarReportTypeViewFilterQuery().Query());
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
