using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineMedicine
{
    public partial class HisMedicineMedicineDAO : EntityBase
    {
        private HisMedicineMedicineCreate CreateWorker
        {
            get
            {
                return (HisMedicineMedicineCreate)Worker.Get<HisMedicineMedicineCreate>();
            }
        }
        private HisMedicineMedicineUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineMedicineUpdate)Worker.Get<HisMedicineMedicineUpdate>();
            }
        }
        private HisMedicineMedicineDelete DeleteWorker
        {
            get
            {
                return (HisMedicineMedicineDelete)Worker.Get<HisMedicineMedicineDelete>();
            }
        }
        private HisMedicineMedicineTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineMedicineTruncate)Worker.Get<HisMedicineMedicineTruncate>();
            }
        }
        private HisMedicineMedicineGet GetWorker
        {
            get
            {
                return (HisMedicineMedicineGet)Worker.Get<HisMedicineMedicineGet>();
            }
        }
        private HisMedicineMedicineCheck CheckWorker
        {
            get
            {
                return (HisMedicineMedicineCheck)Worker.Get<HisMedicineMedicineCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_MEDICINE data)
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

        public bool CreateList(List<HIS_MEDICINE_MEDICINE> listData)
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

        public bool Update(HIS_MEDICINE_MEDICINE data)
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

        public bool UpdateList(List<HIS_MEDICINE_MEDICINE> listData)
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

        public bool Delete(HIS_MEDICINE_MEDICINE data)
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

        public bool DeleteList(List<HIS_MEDICINE_MEDICINE> listData)
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

        public bool Truncate(HIS_MEDICINE_MEDICINE data)
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

        public bool TruncateList(List<HIS_MEDICINE_MEDICINE> listData)
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

        public List<HIS_MEDICINE_MEDICINE> Get(HisMedicineMedicineSO search, CommonParam param)
        {
            List<HIS_MEDICINE_MEDICINE> result = new List<HIS_MEDICINE_MEDICINE>();
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

        public HIS_MEDICINE_MEDICINE GetById(long id, HisMedicineMedicineSO search)
        {
            HIS_MEDICINE_MEDICINE result = null;
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
