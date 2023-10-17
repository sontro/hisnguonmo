using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTypeGroup.Get.V
{
    class SarReportTypeGroupGetVBehaviorById : BeanObjectBase, ISarReportTypeGroupGetV
    {
        long id;

        internal SarReportTypeGroupGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SAR_REPORT_TYPE_GROUP ISarReportTypeGroupGetV.Run()
        {
            try
            {
                return DAOWorker.SarReportTypeGroupDAO.GetViewById(id, new SarReportTypeGroupViewFilterQuery().Query());
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
