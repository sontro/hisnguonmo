using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathWithin
{
    public partial class HisDeathWithinDAO : EntityBase
    {
        public List<V_HIS_DEATH_WITHIN> GetView(HisDeathWithinSO search, CommonParam param)
        {
            List<V_HIS_DEATH_WITHIN> result = new List<V_HIS_DEATH_WITHIN>();
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

        public V_HIS_DEATH_WITHIN GetViewById(long id, HisDeathWithinSO search)
        {
            V_HIS_DEATH_WITHIN result = null;

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
