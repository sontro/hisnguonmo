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
    }
}
