using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarReportTemplate.Create
{
    class SarReportTemplateCreateBehaviorEv : BeanObjectBase, ISarReportTemplateCreate
    {
        SAR_REPORT_TEMPLATE entity;

        internal SarReportTemplateCreateBehaviorEv(CommonParam param, SAR_REPORT_TEMPLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISarReportTemplateCreate.Run()
        {
            bool result = false;
            try
            {
                if (entity.CREATOR == "")
                {
                    entity.CREATOR = null;
                }
                result = Check() && DAOWorker.SarReportTemplateDAO.Create(entity);
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
                result = result && SarReportTemplateCheckVerifyExistsCode.Verify(param, entity.REPORT_TEMPLATE_CODE, null);
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
