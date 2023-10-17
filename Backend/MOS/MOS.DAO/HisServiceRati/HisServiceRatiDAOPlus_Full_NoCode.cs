using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRati
{
    public partial class HisServiceRatiDAO : EntityBase
    {
        public List<V_HIS_SERVICE_RATI> GetView(HisServiceRatiSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_RATI> result = new List<V_HIS_SERVICE_RATI>();
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

        public V_HIS_SERVICE_RATI GetViewById(long id, HisServiceRatiSO search)
        {
            V_HIS_SERVICE_RATI result = null;

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
