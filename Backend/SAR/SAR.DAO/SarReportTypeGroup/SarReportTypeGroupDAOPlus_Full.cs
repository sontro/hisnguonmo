using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTypeGroup
{
    public partial class SarReportTypeGroupDAO : EntityBase
    {
        public List<V_SAR_REPORT_TYPE_GROUP> GetView(SarReportTypeGroupSO search, CommonParam param)
        {
            List<V_SAR_REPORT_TYPE_GROUP> result = new List<V_SAR_REPORT_TYPE_GROUP>();

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

        public SAR_REPORT_TYPE_GROUP GetByCode(string code, SarReportTypeGroupSO search)
        {
            SAR_REPORT_TYPE_GROUP result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
        
        public V_SAR_REPORT_TYPE_GROUP GetViewById(long id, SarReportTypeGroupSO search)
        {
            V_SAR_REPORT_TYPE_GROUP result = null;

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

        public V_SAR_REPORT_TYPE_GROUP GetViewByCode(string code, SarReportTypeGroupSO search)
        {
            V_SAR_REPORT_TYPE_GROUP result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, SAR_REPORT_TYPE_GROUP> GetDicByCode(SarReportTypeGroupSO search, CommonParam param)
        {
            Dictionary<string, SAR_REPORT_TYPE_GROUP> result = new Dictionary<string, SAR_REPORT_TYPE_GROUP>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
