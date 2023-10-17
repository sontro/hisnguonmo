using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisStorageCondition
{
    public partial class HisStorageConditionDAO : EntityBase
    {
        private HisStorageConditionCreate CreateWorker
        {
            get
            {
                return (HisStorageConditionCreate)Worker.Get<HisStorageConditionCreate>();
            }
        }
        private HisStorageConditionUpdate UpdateWorker
        {
            get
            {
                return (HisStorageConditionUpdate)Worker.Get<HisStorageConditionUpdate>();
            }
        }
        private HisStorageConditionDelete DeleteWorker
        {
            get
            {
                return (HisStorageConditionDelete)Worker.Get<HisStorageConditionDelete>();
            }
        }
        private HisStorageConditionTruncate TruncateWorker
        {
            get
            {
                return (HisStorageConditionTruncate)Worker.Get<HisStorageConditionTruncate>();
            }
        }
        private HisStorageConditionGet GetWorker
        {
            get
            {
                return (HisStorageConditionGet)Worker.Get<HisStorageConditionGet>();
            }
        }
        private HisStorageConditionCheck CheckWorker
        {
            get
            {
                return (HisStorageConditionCheck)Worker.Get<HisStorageConditionCheck>();
            }
        }

        public bool Create(HIS_STORAGE_CONDITION data)
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

        public bool CreateList(List<HIS_STORAGE_CONDITION> listData)
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

        public bool Update(HIS_STORAGE_CONDITION data)
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

        public bool UpdateList(List<HIS_STORAGE_CONDITION> listData)
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

        public bool Delete(HIS_STORAGE_CONDITION data)
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

        public bool DeleteList(List<HIS_STORAGE_CONDITION> listData)
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

        public bool Truncate(HIS_STORAGE_CONDITION data)
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

        public bool TruncateList(List<HIS_STORAGE_CONDITION> listData)
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

        public List<HIS_STORAGE_CONDITION> Get(HisStorageConditionSO search, CommonParam param)
        {
            List<HIS_STORAGE_CONDITION> result = new List<HIS_STORAGE_CONDITION>();
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

        public HIS_STORAGE_CONDITION GetById(long id, HisStorageConditionSO search)
        {
            HIS_STORAGE_CONDITION result = null;
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
