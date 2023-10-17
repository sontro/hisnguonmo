using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportStt
{
    public partial class SarReportSttDAO : EntityBase
    {
        public List<V_SAR_REPORT_STT> GetView(SarReportSttSO search, CommonParam param)
        {
            List<V_SAR_REPORT_STT> result = new List<V_SAR_REPORT_STT>();
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

        public V_SAR_REPORT_STT GetViewById(long id, SarReportSttSO search)
        {
            V_SAR_REPORT_STT result = null;

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
