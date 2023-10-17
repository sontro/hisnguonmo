using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientClassify
{
    public partial class HisPatientClassifyDAO : EntityBase
    {
        private HisPatientClassifyCreate CreateWorker
        {
            get
            {
                return (HisPatientClassifyCreate)Worker.Get<HisPatientClassifyCreate>();
            }
        }
        private HisPatientClassifyUpdate UpdateWorker
        {
            get
            {
                return (HisPatientClassifyUpdate)Worker.Get<HisPatientClassifyUpdate>();
            }
        }
        private HisPatientClassifyDelete DeleteWorker
        {
            get
            {
                return (HisPatientClassifyDelete)Worker.Get<HisPatientClassifyDelete>();
            }
        }
        private HisPatientClassifyTruncate TruncateWorker
        {
            get
            {
                return (HisPatientClassifyTruncate)Worker.Get<HisPatientClassifyTruncate>();
            }
        }
        private HisPatientClassifyGet GetWorker
        {
            get
            {
                return (HisPatientClassifyGet)Worker.Get<HisPatientClassifyGet>();
            }
        }
        private HisPatientClassifyCheck CheckWorker
        {
            get
            {
                return (HisPatientClassifyCheck)Worker.Get<HisPatientClassifyCheck>();
            }
        }

        public bool Create(HIS_PATIENT_CLASSIFY data)
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

        public bool CreateList(List<HIS_PATIENT_CLASSIFY> listData)
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

        public bool Update(HIS_PATIENT_CLASSIFY data)
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

        public bool UpdateList(List<HIS_PATIENT_CLASSIFY> listData)
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

        public bool Delete(HIS_PATIENT_CLASSIFY data)
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

        public bool DeleteList(List<HIS_PATIENT_CLASSIFY> listData)
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

        public bool Truncate(HIS_PATIENT_CLASSIFY data)
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

        public bool TruncateList(List<HIS_PATIENT_CLASSIFY> listData)
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

        public List<HIS_PATIENT_CLASSIFY> Get(HisPatientClassifySO search, CommonParam param)
        {
            List<HIS_PATIENT_CLASSIFY> result = new List<HIS_PATIENT_CLASSIFY>();
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

        public HIS_PATIENT_CLASSIFY GetById(long id, HisPatientClassifySO search)
        {
            HIS_PATIENT_CLASSIFY result = null;
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
