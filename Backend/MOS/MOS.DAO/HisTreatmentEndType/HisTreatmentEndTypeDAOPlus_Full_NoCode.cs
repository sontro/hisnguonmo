using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndType
{
    public partial class HisTreatmentEndTypeDAO : EntityBase
    {
        public List<V_HIS_TREATMENT_END_TYPE> GetView(HisTreatmentEndTypeSO search, CommonParam param)
        {
            List<V_HIS_TREATMENT_END_TYPE> result = new List<V_HIS_TREATMENT_END_TYPE>();
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

        public V_HIS_TREATMENT_END_TYPE GetViewById(long id, HisTreatmentEndTypeSO search)
        {
            V_HIS_TREATMENT_END_TYPE result = null;

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
