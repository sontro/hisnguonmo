using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportStt.Create
{
    class SarReportSttCreateBehaviorEv : BeanObjectBase, ISarReportSttCreate
    {
        SAR_REPORT_STT entity;

        internal SarReportSttCreateBehaviorEv(CommonParam param, SAR_REPORT_STT data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportSttCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportSttDAO.Create(entity);
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
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
