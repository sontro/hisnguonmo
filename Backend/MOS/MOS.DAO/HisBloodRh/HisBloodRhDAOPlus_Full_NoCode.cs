using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodRh
{
    public partial class HisBloodRhDAO : EntityBase
    {
        public List<V_HIS_BLOOD_RH> GetView(HisBloodRhSO search, CommonParam param)
        {
            List<V_HIS_BLOOD_RH> result = new List<V_HIS_BLOOD_RH>();
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

        public V_HIS_BLOOD_RH GetViewById(long id, HisBloodRhSO search)
        {
            V_HIS_BLOOD_RH result = null;

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
