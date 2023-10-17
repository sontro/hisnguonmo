using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeAllow
{
    public partial class HisPatientTypeAllowDAO : EntityBase
    {
        private HisPatientTypeAllowCreate CreateWorker
        {
            get
            {
                return (HisPatientTypeAllowCreate)Worker.Get<HisPatientTypeAllowCreate>();
            }
        }
        private HisPatientTypeAllowUpdate UpdateWorker
        {
            get
            {
                return (HisPatientTypeAllowUpdate)Worker.Get<HisPatientTypeAllowUpdate>();
            }
        }
        private HisPatientTypeAllowDelete DeleteWorker
        {
            get
            {
                return (HisPatientTypeAllowDelete)Worker.Get<HisPatientTypeAllowDelete>();
            }
        }
        private HisPatientTypeAllowTruncate TruncateWorker
        {
            get
            {
                return (HisPatientTypeAllowTruncate)Worker.Get<HisPatientTypeAllowTruncate>();
            }
        }
        private HisPatientTypeAllowGet GetWorker
        {
            get
            {
                return (HisPatientTypeAllowGet)Worker.Get<HisPatientTypeAllowGet>();
            }
        }
        private HisPatientTypeAllowCheck CheckWorker
        {
            get
            {
                return (HisPatientTypeAllowCheck)Worker.Get<HisPatientTypeAllowCheck>();
            }
        }

        public bool Create(HIS_PATIENT_TYPE_ALLOW data)
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

        public bool CreateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
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

        public bool Update(HIS_PATIENT_TYPE_ALLOW data)
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

        public bool UpdateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
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

        public bool Delete(HIS_PATIENT_TYPE_ALLOW data)
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

        public bool DeleteList(List<HIS_PATIENT_TYPE_ALLOW> listData)
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

        public bool Truncate(HIS_PATIENT_TYPE_ALLOW data)
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

        public bool TruncateList(List<HIS_PATIENT_TYPE_ALLOW> listData)
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

        public List<HIS_PATIENT_TYPE_ALLOW> Get(HisPatientTypeAllowSO search, CommonParam param)
        {
            List<HIS_PATIENT_TYPE_ALLOW> result = new List<HIS_PATIENT_TYPE_ALLOW>();
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

        public HIS_PATIENT_TYPE_ALLOW GetById(long id, HisPatientTypeAllowSO search)
        {
            HIS_PATIENT_TYPE_ALLOW result = null;
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
