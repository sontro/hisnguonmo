using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientObservation
{
    public partial class HisPatientObservationDAO : EntityBase
    {
        private HisPatientObservationCreate CreateWorker
        {
            get
            {
                return (HisPatientObservationCreate)Worker.Get<HisPatientObservationCreate>();
            }
        }
        private HisPatientObservationUpdate UpdateWorker
        {
            get
            {
                return (HisPatientObservationUpdate)Worker.Get<HisPatientObservationUpdate>();
            }
        }
        private HisPatientObservationDelete DeleteWorker
        {
            get
            {
                return (HisPatientObservationDelete)Worker.Get<HisPatientObservationDelete>();
            }
        }
        private HisPatientObservationTruncate TruncateWorker
        {
            get
            {
                return (HisPatientObservationTruncate)Worker.Get<HisPatientObservationTruncate>();
            }
        }
        private HisPatientObservationGet GetWorker
        {
            get
            {
                return (HisPatientObservationGet)Worker.Get<HisPatientObservationGet>();
            }
        }
        private HisPatientObservationCheck CheckWorker
        {
            get
            {
                return (HisPatientObservationCheck)Worker.Get<HisPatientObservationCheck>();
            }
        }

        public bool Create(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                result = CreateWorker.Create(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool CreateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;
            try
            {
                result = CreateWorker.CreateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Update(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.Update(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool UpdateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;
            try
            {
                result = UpdateWorker.UpdateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Delete(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                result = DeleteWorker.Delete(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool DeleteList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;

            try
            {
                result = DeleteWorker.DeleteList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool Truncate(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.Truncate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public bool TruncateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;
            try
            {
                result = TruncateWorker.TruncateList(listData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public List<HIS_PATIENT_OBSERVATION> Get(HisPatientObservationSO search, CommonParam param)
        {
            List<HIS_PATIENT_OBSERVATION> result = new List<HIS_PATIENT_OBSERVATION>();
            try
            {
                result = GetWorker.Get(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public HIS_PATIENT_OBSERVATION GetById(long id, HisPatientObservationSO search)
        {
            HIS_PATIENT_OBSERVATION result = null;
            try
            {
                result = GetWorker.GetById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public bool IsUnLock(long id)
        {
            try
            {
                return CheckWorker.IsUnLock(id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
