using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisEmteMedicineType
{
    public partial class HisEmteMedicineTypeDAO : EntityBase
    {
        private HisEmteMedicineTypeCreate CreateWorker
        {
            get
            {
                return (HisEmteMedicineTypeCreate)Worker.Get<HisEmteMedicineTypeCreate>();
            }
        }
        private HisEmteMedicineTypeUpdate UpdateWorker
        {
            get
            {
                return (HisEmteMedicineTypeUpdate)Worker.Get<HisEmteMedicineTypeUpdate>();
            }
        }
        private HisEmteMedicineTypeDelete DeleteWorker
        {
            get
            {
                return (HisEmteMedicineTypeDelete)Worker.Get<HisEmteMedicineTypeDelete>();
            }
        }
        private HisEmteMedicineTypeTruncate TruncateWorker
        {
            get
            {
                return (HisEmteMedicineTypeTruncate)Worker.Get<HisEmteMedicineTypeTruncate>();
            }
        }
        private HisEmteMedicineTypeGet GetWorker
        {
            get
            {
                return (HisEmteMedicineTypeGet)Worker.Get<HisEmteMedicineTypeGet>();
            }
        }
        private HisEmteMedicineTypeCheck CheckWorker
        {
            get
            {
                return (HisEmteMedicineTypeCheck)Worker.Get<HisEmteMedicineTypeCheck>();
            }
        }

        public bool Create(HIS_EMTE_MEDICINE_TYPE data)
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

        public bool CreateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
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

        public bool Update(HIS_EMTE_MEDICINE_TYPE data)
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

        public bool UpdateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
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

        public bool Delete(HIS_EMTE_MEDICINE_TYPE data)
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

        public bool DeleteList(List<HIS_EMTE_MEDICINE_TYPE> listData)
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

        public bool Truncate(HIS_EMTE_MEDICINE_TYPE data)
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

        public bool TruncateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
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

        public List<HIS_EMTE_MEDICINE_TYPE> Get(HisEmteMedicineTypeSO search, CommonParam param)
        {
            List<HIS_EMTE_MEDICINE_TYPE> result = new List<HIS_EMTE_MEDICINE_TYPE>();
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

        public HIS_EMTE_MEDICINE_TYPE GetById(long id, HisEmteMedicineTypeSO search)
        {
            HIS_EMTE_MEDICINE_TYPE result = null;
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
