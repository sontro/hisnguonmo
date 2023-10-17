using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAdrMedicineType
{
    public partial class HisAdrMedicineTypeDAO : EntityBase
    {
        private HisAdrMedicineTypeCreate CreateWorker
        {
            get
            {
                return (HisAdrMedicineTypeCreate)Worker.Get<HisAdrMedicineTypeCreate>();
            }
        }
        private HisAdrMedicineTypeUpdate UpdateWorker
        {
            get
            {
                return (HisAdrMedicineTypeUpdate)Worker.Get<HisAdrMedicineTypeUpdate>();
            }
        }
        private HisAdrMedicineTypeDelete DeleteWorker
        {
            get
            {
                return (HisAdrMedicineTypeDelete)Worker.Get<HisAdrMedicineTypeDelete>();
            }
        }
        private HisAdrMedicineTypeTruncate TruncateWorker
        {
            get
            {
                return (HisAdrMedicineTypeTruncate)Worker.Get<HisAdrMedicineTypeTruncate>();
            }
        }
        private HisAdrMedicineTypeGet GetWorker
        {
            get
            {
                return (HisAdrMedicineTypeGet)Worker.Get<HisAdrMedicineTypeGet>();
            }
        }
        private HisAdrMedicineTypeCheck CheckWorker
        {
            get
            {
                return (HisAdrMedicineTypeCheck)Worker.Get<HisAdrMedicineTypeCheck>();
            }
        }

        public bool Create(HIS_ADR_MEDICINE_TYPE data)
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

        public bool CreateList(List<HIS_ADR_MEDICINE_TYPE> listData)
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

        public bool Update(HIS_ADR_MEDICINE_TYPE data)
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

        public bool UpdateList(List<HIS_ADR_MEDICINE_TYPE> listData)
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

        public bool Delete(HIS_ADR_MEDICINE_TYPE data)
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

        public bool DeleteList(List<HIS_ADR_MEDICINE_TYPE> listData)
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

        public bool Truncate(HIS_ADR_MEDICINE_TYPE data)
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

        public bool TruncateList(List<HIS_ADR_MEDICINE_TYPE> listData)
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

        public List<HIS_ADR_MEDICINE_TYPE> Get(HisAdrMedicineTypeSO search, CommonParam param)
        {
            List<HIS_ADR_MEDICINE_TYPE> result = new List<HIS_ADR_MEDICINE_TYPE>();
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

        public HIS_ADR_MEDICINE_TYPE GetById(long id, HisAdrMedicineTypeSO search)
        {
            HIS_ADR_MEDICINE_TYPE result = null;
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
