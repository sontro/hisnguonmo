using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.Ev
{
    class SarUserReportTypeGetEvBehaviorByCode : BeanObjectBase, ISarUserReportTypeGetEv
    {
        string code;

        internal SarUserReportTypeGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SAR_USER_REPORT_TYPE ISarUserReportTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.GetByCode(code, new SarUserReportTypeFilterQuery().Query());
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
