using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDbLog
{
    public partial class HisDbLogDAO : EntityBase
    {
        public List<V_HIS_DB_LOG> GetView(HisDbLogSO search, CommonParam param)
        {
            List<V_HIS_DB_LOG> result = new List<V_HIS_DB_LOG>();
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

        public V_HIS_DB_LOG GetViewById(long id, HisDbLogSO search)
        {
            V_HIS_DB_LOG result = null;

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
