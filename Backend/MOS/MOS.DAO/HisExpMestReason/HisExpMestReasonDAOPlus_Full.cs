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

        public HIS_EXP_MEST_REASON GetByCode(string code, HisExpMestReasonSO search)
        {
            HIS_EXP_MEST_REASON result = null;

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

        public V_HIS_EXP_MEST_REASON GetViewByCode(string code, HisExpMestReasonSO search)
        {
            V_HIS_EXP_MEST_REASON result = null;

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

        public Dictionary<string, HIS_EXP_MEST_REASON> GetDicByCode(HisExpMestReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXP_MEST_REASON> result = new Dictionary<string, HIS_EXP_MEST_REASON>();
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
