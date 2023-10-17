using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRepayReason
{
    public partial class HisRepayReasonDAO : EntityBase
    {
        public List<V_HIS_REPAY_REASON> GetView(HisRepayReasonSO search, CommonParam param)
        {
            List<V_HIS_REPAY_REASON> result = new List<V_HIS_REPAY_REASON>();
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

        public V_HIS_REPAY_REASON GetViewById(long id, HisRepayReasonSO search)
        {
            V_HIS_REPAY_REASON result = null;

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
