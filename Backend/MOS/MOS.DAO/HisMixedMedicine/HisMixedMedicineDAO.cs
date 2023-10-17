using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMixedMedicine
{
    public partial class HisMixedMedicineDAO : EntityBase
    {
        private HisMixedMedicineCreate CreateWorker
        {
            get
            {
                return (HisMixedMedicineCreate)Worker.Get<HisMixedMedicineCreate>();
            }
        }
        private HisMixedMedicineUpdate UpdateWorker
        {
            get
            {
                return (HisMixedMedicineUpdate)Worker.Get<HisMixedMedicineUpdate>();
            }
        }
        private HisMixedMedicineDelete DeleteWorker
        {
            get
            {
                return (HisMixedMedicineDelete)Worker.Get<HisMixedMedicineDelete>();
            }
        }
        private HisMixedMedicineTruncate TruncateWorker
        {
            get
            {
                return (HisMixedMedicineTruncate)Worker.Get<HisMixedMedicineTruncate>();
            }
        }
        private HisMixedMedicineGet GetWorker
        {
            get
            {
                return (HisMixedMedicineGet)Worker.Get<HisMixedMedicineGet>();
            }
        }
        private HisMixedMedicineCheck CheckWorker
        {
            get
            {
                return (HisMixedMedicineCheck)Worker.Get<HisMixedMedicineCheck>();
            }
        }

        public bool Create(HIS_MIXED_MEDICINE data)
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

        public bool CreateList(List<HIS_MIXED_MEDICINE> listData)
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

        public bool Update(HIS_MIXED_MEDICINE data)
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

        public bool UpdateList(List<HIS_MIXED_MEDICINE> listData)
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

        public bool Delete(HIS_MIXED_MEDICINE data)
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

        public bool DeleteList(List<HIS_MIXED_MEDICINE> listData)
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

        public bool Truncate(HIS_MIXED_MEDICINE data)
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

        public bool TruncateList(List<HIS_MIXED_MEDICINE> listData)
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

        public List<HIS_MIXED_MEDICINE> Get(HisMixedMedicineSO search, CommonParam param)
        {
            List<HIS_MIXED_MEDICINE> result = new List<HIS_MIXED_MEDICINE>();
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

        public HIS_MIXED_MEDICINE GetById(long id, HisMixedMedicineSO search)
        {
            HIS_MIXED_MEDICINE result = null;
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
