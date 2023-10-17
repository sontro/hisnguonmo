using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDeathCause
{
    public partial class HisDeathCauseDAO : EntityBase
    {
        public List<V_HIS_DEATH_CAUSE> GetView(HisDeathCauseSO search, CommonParam param)
        {
            List<V_HIS_DEATH_CAUSE> result = new List<V_HIS_DEATH_CAUSE>();
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

        public V_HIS_DEATH_CAUSE GetViewById(long id, HisDeathCauseSO search)
        {
            V_HIS_DEATH_CAUSE result = null;

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
