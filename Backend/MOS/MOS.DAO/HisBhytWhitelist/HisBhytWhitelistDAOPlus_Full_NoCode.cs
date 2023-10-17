using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytWhitelist
{
    public partial class HisBhytWhitelistDAO : EntityBase
    {
        public List<V_HIS_BHYT_WHITELIST> GetView(HisBhytWhitelistSO search, CommonParam param)
        {
            List<V_HIS_BHYT_WHITELIST> result = new List<V_HIS_BHYT_WHITELIST>();
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

        public V_HIS_BHYT_WHITELIST GetViewById(long id, HisBhytWhitelistSO search)
        {
            V_HIS_BHYT_WHITELIST result = null;

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
