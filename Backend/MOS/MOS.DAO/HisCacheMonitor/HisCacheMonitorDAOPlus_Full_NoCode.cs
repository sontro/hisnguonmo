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
    }
}
