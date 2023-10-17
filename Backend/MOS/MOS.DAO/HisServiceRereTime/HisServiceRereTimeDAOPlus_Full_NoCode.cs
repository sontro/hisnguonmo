using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRereTime
{
    public partial class HisServiceRereTimeDAO : EntityBase
    {
        public List<V_HIS_SERVICE_RERE_TIME> GetView(HisServiceRereTimeSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_RERE_TIME> result = new List<V_HIS_SERVICE_RERE_TIME>();
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

        public V_HIS_SERVICE_RERE_TIME GetViewById(long id, HisServiceRereTimeSO search)
        {
            V_HIS_SERVICE_RERE_TIME result = null;

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
