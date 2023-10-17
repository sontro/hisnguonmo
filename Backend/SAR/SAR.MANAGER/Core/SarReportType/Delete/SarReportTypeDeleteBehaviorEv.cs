using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportType.Delete
{
    class SarReportTypeDeleteBehaviorEv : BeanObjectBase, ISarReportTypeDelete
    {
        SAR_REPORT_TYPE entity;

        internal SarReportTypeDeleteBehaviorEv(CommonParam param, SAR_REPORT_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTypeDAO.Truncate(entity);
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
                result = result && SarReportTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
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
