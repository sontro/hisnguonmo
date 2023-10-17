using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentLogType
{
    public partial class HisTreatmentLogTypeDAO : EntityBase
    {
        public List<V_HIS_TREATMENT_LOG_TYPE> GetView(HisTreatmentLogTypeSO search, CommonParam param)
        {
            List<V_HIS_TREATMENT_LOG_TYPE> result = new List<V_HIS_TREATMENT_LOG_TYPE>();
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

        public V_HIS_TREATMENT_LOG_TYPE GetViewById(long id, HisTreatmentLogTypeSO search)
        {
            V_HIS_TREATMENT_LOG_TYPE result = null;

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
