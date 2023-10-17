using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.V
{
    class SarUserReportTypeGetVBehaviorByCode : BeanObjectBase, ISarUserReportTypeGetV
    {
        string code;

        internal SarUserReportTypeGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_USER_REPORT_TYPE ISarUserReportTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.GetViewByCode(code, new SarUserReportTypeViewFilterQuery().Query());
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
