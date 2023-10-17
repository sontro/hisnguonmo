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

        public HIS_NUM_ORDER_ISSUE GetByCode(string code, HisNumOrderIssueSO search)
        {
            HIS_NUM_ORDER_ISSUE result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_NUM_ORDER_ISSUE GetViewByCode(string code, HisNumOrderIssueSO search)
        {
            V_HIS_NUM_ORDER_ISSUE result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_NUM_ORDER_ISSUE> GetDicByCode(HisNumOrderIssueSO search, CommonParam param)
        {
            Dictionary<string, HIS_NUM_ORDER_ISSUE> result = new Dictionary<string, HIS_NUM_ORDER_ISSUE>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
