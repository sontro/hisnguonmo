using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateReason
{
    public partial class HisDebateReasonDAO : EntityBase
    {
        public List<V_HIS_DEBATE_REASON> GetView(HisDebateReasonSO search, CommonParam param)
        {
            List<V_HIS_DEBATE_REASON> result = new List<V_HIS_DEBATE_REASON>();

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

        public HIS_DEBATE_REASON GetByCode(string code, HisDebateReasonSO search)
        {
            HIS_DEBATE_REASON result = null;

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
        
        public V_HIS_DEBATE_REASON GetViewById(long id, HisDebateReasonSO search)
        {
            V_HIS_DEBATE_REASON result = null;

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

        public V_HIS_DEBATE_REASON GetViewByCode(string code, HisDebateReasonSO search)
        {
            V_HIS_DEBATE_REASON result = null;

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

        public Dictionary<string, HIS_DEBATE_REASON> GetDicByCode(HisDebateReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_DEBATE_REASON> result = new Dictionary<string, HIS_DEBATE_REASON>();
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
