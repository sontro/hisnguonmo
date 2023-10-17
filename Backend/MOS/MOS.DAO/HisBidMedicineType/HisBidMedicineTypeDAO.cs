using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBidMedicineType
{
    public partial class HisBidMedicineTypeDAO : EntityBase
    {
        private HisBidMedicineTypeCreate CreateWorker
        {
            get
            {
                return (HisBidMedicineTypeCreate)Worker.Get<HisBidMedicineTypeCreate>();
            }
        }
        private HisBidMedicineTypeUpdate UpdateWorker
        {
            get
            {
                return (HisBidMedicineTypeUpdate)Worker.Get<HisBidMedicineTypeUpdate>();
            }
        }
        private HisBidMedicineTypeDelete DeleteWorker
        {
            get
            {
                return (HisBidMedicineTypeDelete)Worker.Get<HisBidMedicineTypeDelete>();
            }
        }
        private HisBidMedicineTypeTruncate TruncateWorker
        {
            get
            {
                return (HisBidMedicineTypeTruncate)Worker.Get<HisBidMedicineTypeTruncate>();
            }
        }
        private HisBidMedicineTypeGet GetWorker
        {
            get
            {
                return (HisBidMedicineTypeGet)Worker.Get<HisBidMedicineTypeGet>();
            }
        }
        private HisBidMedicineTypeCheck CheckWorker
        {
            get
            {
                return (HisBidMedicineTypeCheck)Worker.Get<HisBidMedicineTypeCheck>();
            }
        }

        public bool Create(HIS_BID_MEDICINE_TYPE data)
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

        public bool CreateList(List<HIS_BID_MEDICINE_TYPE> listData)
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

        public bool Update(HIS_BID_MEDICINE_TYPE data)
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

        public bool UpdateList(List<HIS_BID_MEDICINE_TYPE> listData)
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

        public bool Delete(HIS_BID_MEDICINE_TYPE data)
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

        public bool DeleteList(List<HIS_BID_MEDICINE_TYPE> listData)
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

        public bool Truncate(HIS_BID_MEDICINE_TYPE data)
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

        public bool TruncateList(List<HIS_BID_MEDICINE_TYPE> listData)
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

        public List<HIS_BID_MEDICINE_TYPE> Get(HisBidMedicineTypeSO search, CommonParam param)
        {
            List<HIS_BID_MEDICINE_TYPE> result = new List<HIS_BID_MEDICINE_TYPE>();
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

        public HIS_BID_MEDICINE_TYPE GetById(long id, HisBidMedicineTypeSO search)
        {
            HIS_BID_MEDICINE_TYPE result = null;
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
