using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMilitaryRank
{
    public partial class HisMilitaryRankDAO : EntityBase
    {
        public List<V_HIS_MILITARY_RANK> GetView(HisMilitaryRankSO search, CommonParam param)
        {
            List<V_HIS_MILITARY_RANK> result = new List<V_HIS_MILITARY_RANK>();
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

        public V_HIS_MILITARY_RANK GetViewById(long id, HisMilitaryRankSO search)
        {
            V_HIS_MILITARY_RANK result = null;

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
