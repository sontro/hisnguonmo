using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReason
{
    public partial class HisDepositReasonDAO : EntityBase
    {
        public List<V_HIS_DEPOSIT_REASON> GetView(HisDepositReasonSO search, CommonParam param)
        {
            List<V_HIS_DEPOSIT_REASON> result = new List<V_HIS_DEPOSIT_REASON>();
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

        public V_HIS_DEPOSIT_REASON GetViewById(long id, HisDepositReasonSO search)
        {
            V_HIS_DEPOSIT_REASON result = null;

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
