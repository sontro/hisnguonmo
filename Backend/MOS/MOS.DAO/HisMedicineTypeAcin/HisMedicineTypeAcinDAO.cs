using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeAcin
{
    public partial class HisMedicineTypeAcinDAO : EntityBase
    {
        private HisMedicineTypeAcinCreate CreateWorker
        {
            get
            {
                return (HisMedicineTypeAcinCreate)Worker.Get<HisMedicineTypeAcinCreate>();
            }
        }
        private HisMedicineTypeAcinUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineTypeAcinUpdate)Worker.Get<HisMedicineTypeAcinUpdate>();
            }
        }
        private HisMedicineTypeAcinDelete DeleteWorker
        {
            get
            {
                return (HisMedicineTypeAcinDelete)Worker.Get<HisMedicineTypeAcinDelete>();
            }
        }
        private HisMedicineTypeAcinTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineTypeAcinTruncate)Worker.Get<HisMedicineTypeAcinTruncate>();
            }
        }
        private HisMedicineTypeAcinGet GetWorker
        {
            get
            {
                return (HisMedicineTypeAcinGet)Worker.Get<HisMedicineTypeAcinGet>();
            }
        }
        private HisMedicineTypeAcinCheck CheckWorker
        {
            get
            {
                return (HisMedicineTypeAcinCheck)Worker.Get<HisMedicineTypeAcinCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_TYPE_ACIN data)
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

        public bool CreateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
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

        public bool Update(HIS_MEDICINE_TYPE_ACIN data)
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

        public bool UpdateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
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

        public bool Delete(HIS_MEDICINE_TYPE_ACIN data)
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

        public bool DeleteList(List<HIS_MEDICINE_TYPE_ACIN> listData)
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

        public bool Truncate(HIS_MEDICINE_TYPE_ACIN data)
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

        public bool TruncateList(List<HIS_MEDICINE_TYPE_ACIN> listData)
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

        public List<HIS_MEDICINE_TYPE_ACIN> Get(HisMedicineTypeAcinSO search, CommonParam param)
        {
            List<HIS_MEDICINE_TYPE_ACIN> result = new List<HIS_MEDICINE_TYPE_ACIN>();
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

        public HIS_MEDICINE_TYPE_ACIN GetById(long id, HisMedicineTypeAcinSO search)
        {
            HIS_MEDICINE_TYPE_ACIN result = null;
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
