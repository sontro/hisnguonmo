using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisReportTypeCat
{
    public partial class HisReportTypeCatDAO : EntityBase
    {
        public List<V_HIS_REPORT_TYPE_CAT> GetView(HisReportTypeCatSO search, CommonParam param)
        {
            List<V_HIS_REPORT_TYPE_CAT> result = new List<V_HIS_REPORT_TYPE_CAT>();
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

        public V_HIS_REPORT_TYPE_CAT GetViewById(long id, HisReportTypeCatSO search)
        {
            V_HIS_REPORT_TYPE_CAT result = null;

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
