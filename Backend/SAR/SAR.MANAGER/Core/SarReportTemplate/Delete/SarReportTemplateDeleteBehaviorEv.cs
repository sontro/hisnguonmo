using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate.Delete
{
    class SarReportTemplateDeleteBehaviorEv : BeanObjectBase, ISarReportTemplateDelete
    {
        SAR_REPORT_TEMPLATE entity;

        internal SarReportTemplateDeleteBehaviorEv(CommonParam param, SAR_REPORT_TEMPLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTemplateDAO.Truncate(entity);
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
                result = result && SarReportTemplateCheckVerifyIsUnlock.Verify(param, entity.ID);
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
