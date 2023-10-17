using SAR.DAO.StagingObject;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.DAO.SarReportType
{
    public partial class SarReportTypeDAO : EntityBase
    {
        public List<V_SAR_REPORT_TYPE> GetView(SarReportTypeSO search, CommonParam param)
        {
            List<V_SAR_REPORT_TYPE> result = new List<V_SAR_REPORT_TYPE>();

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

        public SAR_REPORT_TYPE GetByCode(string code, SarReportTypeSO search)
        {
            SAR_REPORT_TYPE result = null;

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
        
        public V_SAR_REPORT_TYPE GetViewById(long id, SarReportTypeSO search)
        {
            V_SAR_REPORT_TYPE result = null;

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

        public V_SAR_REPORT_TYPE GetViewByCode(string code, SarReportTypeSO search)
        {
            V_SAR_REPORT_TYPE result = null;

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

        public Dictionary<string, SAR_REPORT_TYPE> GetDicByCode(SarReportTypeSO search, CommonParam param)
        {
            Dictionary<string, SAR_REPORT_TYPE> result = new Dictionary<string, SAR_REPORT_TYPE>();
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
