using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReport
{
    public partial class SarReportDAO : EntityBase
    {
        public List<V_SAR_REPORT> GetView(SarReportSO search, CommonParam param)
        {
            List<V_SAR_REPORT> result = new List<V_SAR_REPORT>();
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

        public V_SAR_REPORT GetViewById(long id, SarReportSO search)
        {
            V_SAR_REPORT result = null;

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
