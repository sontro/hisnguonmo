using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPatientTypeAlter
{
    public partial class HisPatientTypeAlterDAO : EntityBase
    {
        private HisPatientTypeAlterCreate CreateWorker
        {
            get
            {
                return (HisPatientTypeAlterCreate)Worker.Get<HisPatientTypeAlterCreate>();
            }
        }
        private HisPatientTypeAlterUpdate UpdateWorker
        {
            get
            {
                return (HisPatientTypeAlterUpdate)Worker.Get<HisPatientTypeAlterUpdate>();
            }
        }
        private HisPatientTypeAlterDelete DeleteWorker
        {
            get
            {
                return (HisPatientTypeAlterDelete)Worker.Get<HisPatientTypeAlterDelete>();
            }
        }
        private HisPatientTypeAlterTruncate TruncateWorker
        {
            get
            {
                return (HisPatientTypeAlterTruncate)Worker.Get<HisPatientTypeAlterTruncate>();
            }
        }
        private HisPatientTypeAlterGet GetWorker
        {
            get
            {
                return (HisPatientTypeAlterGet)Worker.Get<HisPatientTypeAlterGet>();
            }
        }
        private HisPatientTypeAlterCheck CheckWorker
        {
            get
            {
                return (HisPatientTypeAlterCheck)Worker.Get<HisPatientTypeAlterCheck>();
            }
        }

        public bool Create(HIS_PATIENT_TYPE_ALTER data)
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

        public bool CreateList(List<HIS_PATIENT_TYPE_ALTER> listData)
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

        public bool Update(HIS_PATIENT_TYPE_ALTER data)
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

        public bool UpdateList(List<HIS_PATIENT_TYPE_ALTER> listData)
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

        public bool Delete(HIS_PATIENT_TYPE_ALTER data)
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

        public bool DeleteList(List<HIS_PATIENT_TYPE_ALTER> listData)
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

        public bool Truncate(HIS_PATIENT_TYPE_ALTER data)
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

        public bool TruncateList(List<HIS_PATIENT_TYPE_ALTER> listData)
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

        public List<HIS_PATIENT_TYPE_ALTER> Get(HisPatientTypeAlterSO search, CommonParam param)
        {
            List<HIS_PATIENT_TYPE_ALTER> result = new List<HIS_PATIENT_TYPE_ALTER>();
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

        public HIS_PATIENT_TYPE_ALTER GetById(long id, HisPatientTypeAlterSO search)
        {
            HIS_PATIENT_TYPE_ALTER result = null;
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
