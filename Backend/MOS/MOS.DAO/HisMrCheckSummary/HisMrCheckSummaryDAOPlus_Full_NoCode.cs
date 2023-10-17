using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckSummary
{
    public partial class HisMrCheckSummaryDAO : EntityBase
    {
        public List<V_HIS_MR_CHECK_SUMMARY> GetView(HisMrCheckSummarySO search, CommonParam param)
        {
            List<V_HIS_MR_CHECK_SUMMARY> result = new List<V_HIS_MR_CHECK_SUMMARY>();
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

        public V_HIS_MR_CHECK_SUMMARY GetViewById(long id, HisMrCheckSummarySO search)
        {
            V_HIS_MR_CHECK_SUMMARY result = null;

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
