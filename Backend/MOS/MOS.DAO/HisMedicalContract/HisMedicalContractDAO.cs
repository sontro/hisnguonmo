using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalContract
{
    public partial class HisMedicalContractDAO : EntityBase
    {
        private HisMedicalContractCreate CreateWorker
        {
            get
            {
                return (HisMedicalContractCreate)Worker.Get<HisMedicalContractCreate>();
            }
        }
        private HisMedicalContractUpdate UpdateWorker
        {
            get
            {
                return (HisMedicalContractUpdate)Worker.Get<HisMedicalContractUpdate>();
            }
        }
        private HisMedicalContractDelete DeleteWorker
        {
            get
            {
                return (HisMedicalContractDelete)Worker.Get<HisMedicalContractDelete>();
            }
        }
        private HisMedicalContractTruncate TruncateWorker
        {
            get
            {
                return (HisMedicalContractTruncate)Worker.Get<HisMedicalContractTruncate>();
            }
        }
        private HisMedicalContractGet GetWorker
        {
            get
            {
                return (HisMedicalContractGet)Worker.Get<HisMedicalContractGet>();
            }
        }
        private HisMedicalContractCheck CheckWorker
        {
            get
            {
                return (HisMedicalContractCheck)Worker.Get<HisMedicalContractCheck>();
            }
        }

        public bool Create(HIS_MEDICAL_CONTRACT data)
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

        public bool CreateList(List<HIS_MEDICAL_CONTRACT> listData)
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

        public bool Update(HIS_MEDICAL_CONTRACT data)
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

        public bool UpdateList(List<HIS_MEDICAL_CONTRACT> listData)
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

        public bool Delete(HIS_MEDICAL_CONTRACT data)
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

        public bool DeleteList(List<HIS_MEDICAL_CONTRACT> listData)
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

        public bool Truncate(HIS_MEDICAL_CONTRACT data)
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

        public bool TruncateList(List<HIS_MEDICAL_CONTRACT> listData)
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

        public List<HIS_MEDICAL_CONTRACT> Get(HisMedicalContractSO search, CommonParam param)
        {
            List<HIS_MEDICAL_CONTRACT> result = new List<HIS_MEDICAL_CONTRACT>();
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

        public HIS_MEDICAL_CONTRACT GetById(long id, HisMedicalContractSO search)
        {
            HIS_MEDICAL_CONTRACT result = null;
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
