using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt.Get.V
{
    class SarReportSttGetVBehaviorByCode : BeanObjectBase, ISarReportSttGetV
    {
        string code;

        internal SarReportSttGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SAR_REPORT_STT ISarReportSttGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportSttDAO.GetViewByCode(code, new SarReportSttViewFilterQuery().Query());
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
