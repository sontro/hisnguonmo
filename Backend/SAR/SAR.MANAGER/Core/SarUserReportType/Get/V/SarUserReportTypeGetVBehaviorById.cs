using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.V
{
    class SarUserReportTypeGetVBehaviorById : BeanObjectBase, ISarUserReportTypeGetV
    {
        long id;

        internal SarUserReportTypeGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_USER_REPORT_TYPE ISarUserReportTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.GetViewById(id, new SarUserReportTypeViewFilterQuery().Query());
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
