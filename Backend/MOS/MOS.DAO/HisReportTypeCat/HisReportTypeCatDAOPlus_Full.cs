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

        public HIS_REPORT_TYPE_CAT GetByCode(string code, HisReportTypeCatSO search)
        {
            HIS_REPORT_TYPE_CAT result = null;

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

        public V_HIS_REPORT_TYPE_CAT GetViewByCode(string code, HisReportTypeCatSO search)
        {
            V_HIS_REPORT_TYPE_CAT result = null;

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

        public Dictionary<string, HIS_REPORT_TYPE_CAT> GetDicByCode(HisReportTypeCatSO search, CommonParam param)
        {
            Dictionary<string, HIS_REPORT_TYPE_CAT> result = new Dictionary<string, HIS_REPORT_TYPE_CAT>();
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
