using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarUserReportType.Get.Ev
{
    class SarUserReportTypeGetEvBehaviorById : BeanObjectBase, ISarUserReportTypeGetEv
    {
        long id;

        internal SarUserReportTypeGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SAR_USER_REPORT_TYPE ISarUserReportTypeGetEv.Run()
        {
            try
            {
                return DAOWorker.SarUserReportTypeDAO.GetById(id, new SarUserReportTypeFilterQuery().Query());
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
