using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmergencyWtime
{
    public partial class HisEmergencyWtimeDAO : EntityBase
    {
        public List<V_HIS_EMERGENCY_WTIME> GetView(HisEmergencyWtimeSO search, CommonParam param)
        {
            List<V_HIS_EMERGENCY_WTIME> result = new List<V_HIS_EMERGENCY_WTIME>();
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

        public V_HIS_EMERGENCY_WTIME GetViewById(long id, HisEmergencyWtimeSO search)
        {
            V_HIS_EMERGENCY_WTIME result = null;

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
