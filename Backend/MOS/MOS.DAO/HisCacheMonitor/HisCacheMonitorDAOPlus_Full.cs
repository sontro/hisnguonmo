using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCacheMonitor
{
    public partial class HisCacheMonitorDAO : EntityBase
    {
        public List<V_HIS_CACHE_MONITOR> GetView(HisCacheMonitorSO search, CommonParam param)
        {
            List<V_HIS_CACHE_MONITOR> result = new List<V_HIS_CACHE_MONITOR>();

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

        public HIS_CACHE_MONITOR GetByCode(string code, HisCacheMonitorSO search)
        {
            HIS_CACHE_MONITOR result = null;

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
        
        public V_HIS_CACHE_MONITOR GetViewById(long id, HisCacheMonitorSO search)
        {
            V_HIS_CACHE_MONITOR result = null;

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

        public V_HIS_CACHE_MONITOR GetViewByCode(string code, HisCacheMonitorSO search)
        {
            V_HIS_CACHE_MONITOR result = null;

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

        public Dictionary<string, HIS_CACHE_MONITOR> GetDicByCode(HisCacheMonitorSO search, CommonParam param)
        {
            Dictionary<string, HIS_CACHE_MONITOR> result = new Dictionary<string, HIS_CACHE_MONITOR>();
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
