using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentResult
{
    public partial class HisAccidentResultDAO : EntityBase
    {
        public List<V_HIS_ACCIDENT_RESULT> GetView(HisAccidentResultSO search, CommonParam param)
        {
            List<V_HIS_ACCIDENT_RESULT> result = new List<V_HIS_ACCIDENT_RESULT>();

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

        public HIS_ACCIDENT_RESULT GetByCode(string code, HisAccidentResultSO search)
        {
            HIS_ACCIDENT_RESULT result = null;

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
        
        public V_HIS_ACCIDENT_RESULT GetViewById(long id, HisAccidentResultSO search)
        {
            V_HIS_ACCIDENT_RESULT result = null;

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

        public V_HIS_ACCIDENT_RESULT GetViewByCode(string code, HisAccidentResultSO search)
        {
            V_HIS_ACCIDENT_RESULT result = null;

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

        public Dictionary<string, HIS_ACCIDENT_RESULT> GetDicByCode(HisAccidentResultSO search, CommonParam param)
        {
            Dictionary<string, HIS_ACCIDENT_RESULT> result = new Dictionary<string, HIS_ACCIDENT_RESULT>();
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
