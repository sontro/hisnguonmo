using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeTut
{
    public partial class HisMedicineTypeTutDAO : EntityBase
    {
        private HisMedicineTypeTutCreate CreateWorker
        {
            get
            {
                return (HisMedicineTypeTutCreate)Worker.Get<HisMedicineTypeTutCreate>();
            }
        }
        private HisMedicineTypeTutUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineTypeTutUpdate)Worker.Get<HisMedicineTypeTutUpdate>();
            }
        }
        private HisMedicineTypeTutDelete DeleteWorker
        {
            get
            {
                return (HisMedicineTypeTutDelete)Worker.Get<HisMedicineTypeTutDelete>();
            }
        }
        private HisMedicineTypeTutTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineTypeTutTruncate)Worker.Get<HisMedicineTypeTutTruncate>();
            }
        }
        private HisMedicineTypeTutGet GetWorker
        {
            get
            {
                return (HisMedicineTypeTutGet)Worker.Get<HisMedicineTypeTutGet>();
            }
        }
        private HisMedicineTypeTutCheck CheckWorker
        {
            get
            {
                return (HisMedicineTypeTutCheck)Worker.Get<HisMedicineTypeTutCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_TYPE_TUT data)
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

        public bool CreateList(List<HIS_MEDICINE_TYPE_TUT> listData)
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

        public bool Update(HIS_MEDICINE_TYPE_TUT data)
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

        public bool UpdateList(List<HIS_MEDICINE_TYPE_TUT> listData)
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

        public bool Delete(HIS_MEDICINE_TYPE_TUT data)
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

        public bool DeleteList(List<HIS_MEDICINE_TYPE_TUT> listData)
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

        public bool Truncate(HIS_MEDICINE_TYPE_TUT data)
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

        public bool TruncateList(List<HIS_MEDICINE_TYPE_TUT> listData)
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

        public List<HIS_MEDICINE_TYPE_TUT> Get(HisMedicineTypeTutSO search, CommonParam param)
        {
            List<HIS_MEDICINE_TYPE_TUT> result = new List<HIS_MEDICINE_TYPE_TUT>();
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

        public HIS_MEDICINE_TYPE_TUT GetById(long id, HisMedicineTypeTutSO search)
        {
            HIS_MEDICINE_TYPE_TUT result = null;
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
