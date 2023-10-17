using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt.Get.V
{
    class SarReportSttGetVBehaviorById : BeanObjectBase, ISarReportSttGetV
    {
        long id;

        internal SarReportSttGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_REPORT_STT ISarReportSttGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportSttDAO.GetViewById(id, new SarReportSttViewFilterQuery().Query());
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
