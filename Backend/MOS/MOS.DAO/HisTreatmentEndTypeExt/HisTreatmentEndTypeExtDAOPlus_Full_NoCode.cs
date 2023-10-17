using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    public partial class HisTreatmentEndTypeExtDAO : EntityBase
    {
        public List<V_HIS_TREATMENT_END_TYPE_EXT> GetView(HisTreatmentEndTypeExtSO search, CommonParam param)
        {
            List<V_HIS_TREATMENT_END_TYPE_EXT> result = new List<V_HIS_TREATMENT_END_TYPE_EXT>();
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

        public V_HIS_TREATMENT_END_TYPE_EXT GetViewById(long id, HisTreatmentEndTypeExtSO search)
        {
            V_HIS_TREATMENT_END_TYPE_EXT result = null;

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
