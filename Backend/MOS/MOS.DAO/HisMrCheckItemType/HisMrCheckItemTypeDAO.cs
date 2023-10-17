using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItemType
{
    public partial class HisMrCheckItemTypeDAO : EntityBase
    {
        private HisMrCheckItemTypeCreate CreateWorker
        {
            get
            {
                return (HisMrCheckItemTypeCreate)Worker.Get<HisMrCheckItemTypeCreate>();
            }
        }
        private HisMrCheckItemTypeUpdate UpdateWorker
        {
            get
            {
                return (HisMrCheckItemTypeUpdate)Worker.Get<HisMrCheckItemTypeUpdate>();
            }
        }
        private HisMrCheckItemTypeDelete DeleteWorker
        {
            get
            {
                return (HisMrCheckItemTypeDelete)Worker.Get<HisMrCheckItemTypeDelete>();
            }
        }
        private HisMrCheckItemTypeTruncate TruncateWorker
        {
            get
            {
                return (HisMrCheckItemTypeTruncate)Worker.Get<HisMrCheckItemTypeTruncate>();
            }
        }
        private HisMrCheckItemTypeGet GetWorker
        {
            get
            {
                return (HisMrCheckItemTypeGet)Worker.Get<HisMrCheckItemTypeGet>();
            }
        }
        private HisMrCheckItemTypeCheck CheckWorker
        {
            get
            {
                return (HisMrCheckItemTypeCheck)Worker.Get<HisMrCheckItemTypeCheck>();
            }
        }

        public bool Create(HIS_MR_CHECK_ITEM_TYPE data)
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

        public bool CreateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
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

        public bool Update(HIS_MR_CHECK_ITEM_TYPE data)
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

        public bool UpdateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
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

        public bool Delete(HIS_MR_CHECK_ITEM_TYPE data)
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

        public bool DeleteList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
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

        public bool Truncate(HIS_MR_CHECK_ITEM_TYPE data)
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

        public bool TruncateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
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

        public List<HIS_MR_CHECK_ITEM_TYPE> Get(HisMrCheckItemTypeSO search, CommonParam param)
        {
            List<HIS_MR_CHECK_ITEM_TYPE> result = new List<HIS_MR_CHECK_ITEM_TYPE>();
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

        public HIS_MR_CHECK_ITEM_TYPE GetById(long id, HisMrCheckItemTypeSO search)
        {
            HIS_MR_CHECK_ITEM_TYPE result = null;
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
