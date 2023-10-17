using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SarReportTemplate.Create
{
    class SarReportTemplateCreateBehaviorListEv : BeanObjectBase, ISarReportTemplateCreate
    {
        List<SAR_REPORT_TEMPLATE> entities;

        internal SarReportTemplateCreateBehaviorListEv(CommonParam param, List<SAR_REPORT_TEMPLATE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISarReportTemplateCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SarReportTemplateDAO.CreateList(entities);
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
                result = result && SarReportTemplateCheckVerifyValidData.Verify(param, entities);
                foreach (var item in entities)
                {
                    result = result && SarReportTemplateCheckVerifyExistsCode.Verify(param, item.REPORT_TEMPLATE_CODE, null);
                    if (!result) break;
                }
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
