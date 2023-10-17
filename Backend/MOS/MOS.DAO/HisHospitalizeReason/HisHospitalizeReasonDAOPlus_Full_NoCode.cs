using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHospitalizeReason
{
    public partial class HisHospitalizeReasonDAO : EntityBase
    {
        public List<V_HIS_HOSPITALIZE_REASON> GetView(HisHospitalizeReasonSO search, CommonParam param)
        {
            List<V_HIS_HOSPITALIZE_REASON> result = new List<V_HIS_HOSPITALIZE_REASON>();
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

        public V_HIS_HOSPITALIZE_REASON GetViewById(long id, HisHospitalizeReasonSO search)
        {
            V_HIS_HOSPITALIZE_REASON result = null;

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
