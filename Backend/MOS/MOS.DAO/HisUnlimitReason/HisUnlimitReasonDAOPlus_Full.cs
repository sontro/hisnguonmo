using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUnlimitReason
{
    public partial class HisUnlimitReasonDAO : EntityBase
    {
        public List<V_HIS_UNLIMIT_REASON> GetView(HisUnlimitReasonSO search, CommonParam param)
        {
            List<V_HIS_UNLIMIT_REASON> result = new List<V_HIS_UNLIMIT_REASON>();

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

        public HIS_UNLIMIT_REASON GetByCode(string code, HisUnlimitReasonSO search)
        {
            HIS_UNLIMIT_REASON result = null;

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
        
        public V_HIS_UNLIMIT_REASON GetViewById(long id, HisUnlimitReasonSO search)
        {
            V_HIS_UNLIMIT_REASON result = null;

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

        public V_HIS_UNLIMIT_REASON GetViewByCode(string code, HisUnlimitReasonSO search)
        {
            V_HIS_UNLIMIT_REASON result = null;

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

        public Dictionary<string, HIS_UNLIMIT_REASON> GetDicByCode(HisUnlimitReasonSO search, CommonParam param)
        {
            Dictionary<string, HIS_UNLIMIT_REASON> result = new Dictionary<string, HIS_UNLIMIT_REASON>();
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
