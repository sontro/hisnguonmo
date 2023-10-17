using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationTime
{
    public partial class HisRationTimeDAO : EntityBase
    {
        public List<V_HIS_RATION_TIME> GetView(HisRationTimeSO search, CommonParam param)
        {
            List<V_HIS_RATION_TIME> result = new List<V_HIS_RATION_TIME>();
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

        public V_HIS_RATION_TIME GetViewById(long id, HisRationTimeSO search)
        {
            V_HIS_RATION_TIME result = null;

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
