using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt.Update
{
    class SarReportSttUpdateBehaviorEv : BeanObjectBase, ISarReportSttUpdate
    {
        SAR_REPORT_STT entity;

        internal SarReportSttUpdateBehaviorEv(CommonParam param, SAR_REPORT_STT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportSttUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportSttDAO.Update(entity);
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
                result = result && SarReportSttCheckVerifyValidData.Verify(param, entity);
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
