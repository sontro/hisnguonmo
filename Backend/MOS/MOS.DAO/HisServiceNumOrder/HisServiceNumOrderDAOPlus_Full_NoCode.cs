using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceNumOrder
{
    public partial class HisServiceNumOrderDAO : EntityBase
    {
        public List<V_HIS_SERVICE_NUM_ORDER> GetView(HisServiceNumOrderSO search, CommonParam param)
        {
            List<V_HIS_SERVICE_NUM_ORDER> result = new List<V_HIS_SERVICE_NUM_ORDER>();
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

        public V_HIS_SERVICE_NUM_ORDER GetViewById(long id, HisServiceNumOrderSO search)
        {
            V_HIS_SERVICE_NUM_ORDER result = null;

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
