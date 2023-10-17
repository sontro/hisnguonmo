using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentUnlimit
{
    public partial class HisTreatmentUnlimitDAO : EntityBase
    {
        private HisTreatmentUnlimitCreate CreateWorker
        {
            get
            {
                return (HisTreatmentUnlimitCreate)Worker.Get<HisTreatmentUnlimitCreate>();
            }
        }
        private HisTreatmentUnlimitUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentUnlimitUpdate)Worker.Get<HisTreatmentUnlimitUpdate>();
            }
        }
        private HisTreatmentUnlimitDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentUnlimitDelete)Worker.Get<HisTreatmentUnlimitDelete>();
            }
        }
        private HisTreatmentUnlimitTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentUnlimitTruncate)Worker.Get<HisTreatmentUnlimitTruncate>();
            }
        }
        private HisTreatmentUnlimitGet GetWorker
        {
            get
            {
                return (HisTreatmentUnlimitGet)Worker.Get<HisTreatmentUnlimitGet>();
            }
        }
        private HisTreatmentUnlimitCheck CheckWorker
        {
            get
            {
                return (HisTreatmentUnlimitCheck)Worker.Get<HisTreatmentUnlimitCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_UNLIMIT data)
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

        public bool CreateList(List<HIS_TREATMENT_UNLIMIT> listData)
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

        public bool Update(HIS_TREATMENT_UNLIMIT data)
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

        public bool UpdateList(List<HIS_TREATMENT_UNLIMIT> listData)
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

        public bool Delete(HIS_TREATMENT_UNLIMIT data)
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

        public bool DeleteList(List<HIS_TREATMENT_UNLIMIT> listData)
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

        public bool Truncate(HIS_TREATMENT_UNLIMIT data)
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

        public bool TruncateList(List<HIS_TREATMENT_UNLIMIT> listData)
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

        public List<HIS_TREATMENT_UNLIMIT> Get(HisTreatmentUnlimitSO search, CommonParam param)
        {
            List<HIS_TREATMENT_UNLIMIT> result = new List<HIS_TREATMENT_UNLIMIT>();
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

        public HIS_TREATMENT_UNLIMIT GetById(long id, HisTreatmentUnlimitSO search)
        {
            HIS_TREATMENT_UNLIMIT result = null;
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
