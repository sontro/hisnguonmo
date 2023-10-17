using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeSub
{
    public partial class HisPatientTypeSubDAO : EntityBase
    {
        private HisPatientTypeSubCreate CreateWorker
        {
            get
            {
                return (HisPatientTypeSubCreate)Worker.Get<HisPatientTypeSubCreate>();
            }
        }
        private HisPatientTypeSubUpdate UpdateWorker
        {
            get
            {
                return (HisPatientTypeSubUpdate)Worker.Get<HisPatientTypeSubUpdate>();
            }
        }
        private HisPatientTypeSubDelete DeleteWorker
        {
            get
            {
                return (HisPatientTypeSubDelete)Worker.Get<HisPatientTypeSubDelete>();
            }
        }
        private HisPatientTypeSubTruncate TruncateWorker
        {
            get
            {
                return (HisPatientTypeSubTruncate)Worker.Get<HisPatientTypeSubTruncate>();
            }
        }
        private HisPatientTypeSubGet GetWorker
        {
            get
            {
                return (HisPatientTypeSubGet)Worker.Get<HisPatientTypeSubGet>();
            }
        }
        private HisPatientTypeSubCheck CheckWorker
        {
            get
            {
                return (HisPatientTypeSubCheck)Worker.Get<HisPatientTypeSubCheck>();
            }
        }

        public bool Create(HIS_PATIENT_TYPE_SUB data)
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

        public bool CreateList(List<HIS_PATIENT_TYPE_SUB> listData)
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

        public bool Update(HIS_PATIENT_TYPE_SUB data)
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

        public bool UpdateList(List<HIS_PATIENT_TYPE_SUB> listData)
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

        public bool Delete(HIS_PATIENT_TYPE_SUB data)
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

        public bool DeleteList(List<HIS_PATIENT_TYPE_SUB> listData)
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

        public bool Truncate(HIS_PATIENT_TYPE_SUB data)
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

        public bool TruncateList(List<HIS_PATIENT_TYPE_SUB> listData)
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

        public List<HIS_PATIENT_TYPE_SUB> Get(HisPatientTypeSubSO search, CommonParam param)
        {
            List<HIS_PATIENT_TYPE_SUB> result = new List<HIS_PATIENT_TYPE_SUB>();
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

        public HIS_PATIENT_TYPE_SUB GetById(long id, HisPatientTypeSubSO search)
        {
            HIS_PATIENT_TYPE_SUB result = null;
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
