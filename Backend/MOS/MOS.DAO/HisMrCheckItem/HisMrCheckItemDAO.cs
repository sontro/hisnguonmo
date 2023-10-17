using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckItem
{
    public partial class HisMrCheckItemDAO : EntityBase
    {
        private HisMrCheckItemCreate CreateWorker
        {
            get
            {
                return (HisMrCheckItemCreate)Worker.Get<HisMrCheckItemCreate>();
            }
        }
        private HisMrCheckItemUpdate UpdateWorker
        {
            get
            {
                return (HisMrCheckItemUpdate)Worker.Get<HisMrCheckItemUpdate>();
            }
        }
        private HisMrCheckItemDelete DeleteWorker
        {
            get
            {
                return (HisMrCheckItemDelete)Worker.Get<HisMrCheckItemDelete>();
            }
        }
        private HisMrCheckItemTruncate TruncateWorker
        {
            get
            {
                return (HisMrCheckItemTruncate)Worker.Get<HisMrCheckItemTruncate>();
            }
        }
        private HisMrCheckItemGet GetWorker
        {
            get
            {
                return (HisMrCheckItemGet)Worker.Get<HisMrCheckItemGet>();
            }
        }
        private HisMrCheckItemCheck CheckWorker
        {
            get
            {
                return (HisMrCheckItemCheck)Worker.Get<HisMrCheckItemCheck>();
            }
        }

        public bool Create(HIS_MR_CHECK_ITEM data)
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

        public bool CreateList(List<HIS_MR_CHECK_ITEM> listData)
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

        public bool Update(HIS_MR_CHECK_ITEM data)
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

        public bool UpdateList(List<HIS_MR_CHECK_ITEM> listData)
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

        public bool Delete(HIS_MR_CHECK_ITEM data)
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

        public bool DeleteList(List<HIS_MR_CHECK_ITEM> listData)
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

        public bool Truncate(HIS_MR_CHECK_ITEM data)
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

        public bool TruncateList(List<HIS_MR_CHECK_ITEM> listData)
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

        public List<HIS_MR_CHECK_ITEM> Get(HisMrCheckItemSO search, CommonParam param)
        {
            List<HIS_MR_CHECK_ITEM> result = new List<HIS_MR_CHECK_ITEM>();
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

        public HIS_MR_CHECK_ITEM GetById(long id, HisMrCheckItemSO search)
        {
            HIS_MR_CHECK_ITEM result = null;
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
