using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineUseForm
{
    public partial class HisMedicineUseFormDAO : EntityBase
    {
        private HisMedicineUseFormCreate CreateWorker
        {
            get
            {
                return (HisMedicineUseFormCreate)Worker.Get<HisMedicineUseFormCreate>();
            }
        }
        private HisMedicineUseFormUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineUseFormUpdate)Worker.Get<HisMedicineUseFormUpdate>();
            }
        }
        private HisMedicineUseFormDelete DeleteWorker
        {
            get
            {
                return (HisMedicineUseFormDelete)Worker.Get<HisMedicineUseFormDelete>();
            }
        }
        private HisMedicineUseFormTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineUseFormTruncate)Worker.Get<HisMedicineUseFormTruncate>();
            }
        }
        private HisMedicineUseFormGet GetWorker
        {
            get
            {
                return (HisMedicineUseFormGet)Worker.Get<HisMedicineUseFormGet>();
            }
        }
        private HisMedicineUseFormCheck CheckWorker
        {
            get
            {
                return (HisMedicineUseFormCheck)Worker.Get<HisMedicineUseFormCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_USE_FORM data)
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

        public bool CreateList(List<HIS_MEDICINE_USE_FORM> listData)
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

        public bool Update(HIS_MEDICINE_USE_FORM data)
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

        public bool UpdateList(List<HIS_MEDICINE_USE_FORM> listData)
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

        public bool Delete(HIS_MEDICINE_USE_FORM data)
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

        public bool DeleteList(List<HIS_MEDICINE_USE_FORM> listData)
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

        public bool Truncate(HIS_MEDICINE_USE_FORM data)
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

        public bool TruncateList(List<HIS_MEDICINE_USE_FORM> listData)
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

        public List<HIS_MEDICINE_USE_FORM> Get(HisMedicineUseFormSO search, CommonParam param)
        {
            List<HIS_MEDICINE_USE_FORM> result = new List<HIS_MEDICINE_USE_FORM>();
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

        public HIS_MEDICINE_USE_FORM GetById(long id, HisMedicineUseFormSO search)
        {
            HIS_MEDICINE_USE_FORM result = null;
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
