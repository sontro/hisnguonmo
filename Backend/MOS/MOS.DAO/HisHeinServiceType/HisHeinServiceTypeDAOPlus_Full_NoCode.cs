using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReport
{
    public partial class HisServiceReportDAO : EntityBase
    {
        public List<V_HIS_SERVICE_REPORT> GetView(HisServiceReportSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_REPORT> result = new List<V_HIS_SERVICE_REPORT>();
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

        public V_HIS_SERVICE_REPORT GetViewById(long id, HisServiceReportSO search)
        {
            V_HIS_SERVICE_REPORT result = null;

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
