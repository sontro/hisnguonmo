using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTemplate
{
    public partial class SarReportTemplateDAO : EntityBase
    {
        public List<V_SAR_REPORT_TEMPLATE> GetView(SarReportTemplateSO search, CommonParam param)
        {
            List<V_SAR_REPORT_TEMPLATE> result = new List<V_SAR_REPORT_TEMPLATE>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_SAR_REPORT_TEMPLATE GetViewById(long id, SarReportTemplateSO search)
        {
            V_SAR_REPORT_TEMPLATE result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
