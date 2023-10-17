using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReport.Delete
{
    class SarReportDeleteBehaviorEv : BeanObjectBase, ISarReportDelete
    {
        SAR_REPORT entity;

        internal SarReportDeleteBehaviorEv(CommonParam param, SAR_REPORT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportDAO.Truncate(entity);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SarReportCheckVerifyIsUnlock.Verify(param, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
