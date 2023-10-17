using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType.Get.V
{
    class SarReportTypeGetVBehaviorById : BeanObjectBase, ISarReportTypeGetV
    {
        long id;

        internal SarReportTypeGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_REPORT_TYPE ISarReportTypeGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeDAO.GetViewById(id, new SarReportTypeViewFilterQuery().Query());
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
