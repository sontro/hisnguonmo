using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt.Delete
{
    class SarReportSttDeleteBehaviorEv : BeanObjectBase, ISarReportSttDelete
    {
        SAR_REPORT_STT entity;

        internal SarReportSttDeleteBehaviorEv(CommonParam param, SAR_REPORT_STT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportSttDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportSttDAO.Truncate(entity);
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
                result = result && SarReportSttCheckVerifyIsUnlock.Verify(param, entity.ID);
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
