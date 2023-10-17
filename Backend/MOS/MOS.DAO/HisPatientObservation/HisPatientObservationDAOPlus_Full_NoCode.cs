using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientObservation
{
    public partial class HisPatientObservationDAO : EntityBase
    {
        public List<V_HIS_PATIENT_OBSERVATION> GetView(HisPatientObservationSO search, CommonParam param)
        {
            List<V_HIS_PATIENT_OBSERVATION> result = new List<V_HIS_PATIENT_OBSERVATION>();
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

        public V_HIS_PATIENT_OBSERVATION GetViewById(long id, HisPatientObservationSO search)
        {
            V_HIS_PATIENT_OBSERVATION result = null;

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
