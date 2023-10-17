using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathDAO : EntityBase
    {
        public List<V_HIS_EVENTS_CAUSES_DEATH> GetView(HisEventsCausesDeathSO search, CommonParam param)
        {
            List<V_HIS_EVENTS_CAUSES_DEATH> result = new List<V_HIS_EVENTS_CAUSES_DEATH>();
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

        public V_HIS_EVENTS_CAUSES_DEATH GetViewById(long id, HisEventsCausesDeathSO search)
        {
            V_HIS_EVENTS_CAUSES_DEATH result = null;

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
