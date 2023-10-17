using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRationSum
{
    public partial class HisRationSumDAO : EntityBase
    {
        public List<V_HIS_RATION_SUM> GetView(HisRationSumSO search, CommonParam param)
        {
            List<V_HIS_RATION_SUM> result = new List<V_HIS_RATION_SUM>();
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

        public V_HIS_RATION_SUM GetViewById(long id, HisRationSumSO search)
        {
            V_HIS_RATION_SUM result = null;

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
