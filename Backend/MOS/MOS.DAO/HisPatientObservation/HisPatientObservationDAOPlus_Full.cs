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

        public HIS_PATIENT_OBSERVATION GetByCode(string code, HisPatientObservationSO search)
        {
            HIS_PATIENT_OBSERVATION result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_PATIENT_OBSERVATION GetViewByCode(string code, HisPatientObservationSO search)
        {
            V_HIS_PATIENT_OBSERVATION result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_PATIENT_OBSERVATION> GetDicByCode(HisPatientObservationSO search, CommonParam param)
        {
            Dictionary<string, HIS_PATIENT_OBSERVATION> result = new Dictionary<string, HIS_PATIENT_OBSERVATION>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
