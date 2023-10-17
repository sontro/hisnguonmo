using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisReportTypeCat
{
    public partial class HisReportTypeCatDAO : EntityBase
    {
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
