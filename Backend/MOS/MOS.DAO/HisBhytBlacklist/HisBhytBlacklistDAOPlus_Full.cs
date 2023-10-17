using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytBlacklist
{
    public partial class HisBhytBlacklistDAO : EntityBase
    {
        public List<V_HIS_BHYT_BLACKLIST> GetView(HisBhytBlacklistSO search, CommonParam param)
        {
            List<V_HIS_BHYT_BLACKLIST> result = new List<V_HIS_BHYT_BLACKLIST>();

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

        public HIS_BHYT_BLACKLIST GetByCode(string code, HisBhytBlacklistSO search)
        {
            HIS_BHYT_BLACKLIST result = null;

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
        
        public V_HIS_BHYT_BLACKLIST GetViewById(long id, HisBhytBlacklistSO search)
        {
            V_HIS_BHYT_BLACKLIST result = null;

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

        public V_HIS_BHYT_BLACKLIST GetViewByCode(string code, HisBhytBlacklistSO search)
        {
            V_HIS_BHYT_BLACKLIST result = null;

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

        public Dictionary<string, HIS_BHYT_BLACKLIST> GetDicByCode(HisBhytBlacklistSO search, CommonParam param)
        {
            Dictionary<string, HIS_BHYT_BLACKLIST> result = new Dictionary<string, HIS_BHYT_BLACKLIST>();
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
