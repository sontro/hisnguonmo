using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestReason
{
    public partial class HisExpMestReasonDAO : EntityBase
    {
        public List<V_HIS_EXP_MEST_REASON> GetView(HisExpMestReasonSO search, CommonParam param)
        {
            List<V_HIS_EXP_MEST_REASON> result = new List<V_HIS_EXP_MEST_REASON>();
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

        public V_HIS_EXP_MEST_REASON GetViewById(long id, HisExpMestReasonSO search)
        {
            V_HIS_EXP_MEST_REASON result = null;

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
