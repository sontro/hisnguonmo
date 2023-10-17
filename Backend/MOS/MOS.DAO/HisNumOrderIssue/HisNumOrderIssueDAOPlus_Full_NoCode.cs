using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderIssue
{
    public partial class HisNumOrderIssueDAO : EntityBase
    {
        public List<V_HIS_NUM_ORDER_ISSUE> GetView(HisNumOrderIssueSO search, CommonParam param)
        {
            List<V_HIS_NUM_ORDER_ISSUE> result = new List<V_HIS_NUM_ORDER_ISSUE>();
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

        public V_HIS_NUM_ORDER_ISSUE GetViewById(long id, HisNumOrderIssueSO search)
        {
            V_HIS_NUM_ORDER_ISSUE result = null;

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
