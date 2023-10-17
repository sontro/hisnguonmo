using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate.Update
{
    class SarReportTemplateUpdateBehaviorEv : BeanObjectBase, ISarReportTemplateUpdate
    {
        SAR_REPORT_TEMPLATE entity;

        internal SarReportTemplateUpdateBehaviorEv(CommonParam param, SAR_REPORT_TEMPLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTemplateDAO.Update(entity);
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
                result = result && SarReportTemplateCheckVerifyValidData.Verify(param, entity);
                result = result && SarReportTemplateCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && SarReportTemplateCheckVerifyExistsCode.Verify(param, entity.REPORT_TEMPLATE_CODE, entity.ID);
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
