using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEkipPlan
{
    public partial class HisEkipPlanDAO : EntityBase
    {
        public List<V_HIS_EKIP_PLAN> GetView(HisEkipPlanSO search, CommonParam param)
        {
            List<V_HIS_EKIP_PLAN> result = new List<V_HIS_EKIP_PLAN>();
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

        public V_HIS_EKIP_PLAN GetViewById(long id, HisEkipPlanSO search)
        {
            V_HIS_EKIP_PLAN result = null;

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
