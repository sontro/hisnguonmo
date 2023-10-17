using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytBlacklist
{
    public partial class HisBhytBlacklistDAO : EntityBase
    {
        private HisBhytBlacklistCreate CreateWorker
        {
            get
            {
                return (HisBhytBlacklistCreate)Worker.Get<HisBhytBlacklistCreate>();
            }
        }
        private HisBhytBlacklistUpdate UpdateWorker
        {
            get
            {
                return (HisBhytBlacklistUpdate)Worker.Get<HisBhytBlacklistUpdate>();
            }
        }
        private HisBhytBlacklistDelete DeleteWorker
        {
            get
            {
                return (HisBhytBlacklistDelete)Worker.Get<HisBhytBlacklistDelete>();
            }
        }
        private HisBhytBlacklistTruncate TruncateWorker
        {
            get
            {
                return (HisBhytBlacklistTruncate)Worker.Get<HisBhytBlacklistTruncate>();
            }
        }
        private HisBhytBlacklistGet GetWorker
        {
            get
            {
                return (HisBhytBlacklistGet)Worker.Get<HisBhytBlacklistGet>();
            }
        }
        private HisBhytBlacklistCheck CheckWorker
        {
            get
            {
                return (HisBhytBlacklistCheck)Worker.Get<HisBhytBlacklistCheck>();
            }
        }

        public bool Create(HIS_BHYT_BLACKLIST data)
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

        public bool CreateList(List<HIS_BHYT_BLACKLIST> listData)
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

        public bool Update(HIS_BHYT_BLACKLIST data)
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

        public bool UpdateList(List<HIS_BHYT_BLACKLIST> listData)
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

        public bool Delete(HIS_BHYT_BLACKLIST data)
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

        public bool DeleteList(List<HIS_BHYT_BLACKLIST> listData)
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

        public bool Truncate(HIS_BHYT_BLACKLIST data)
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

        public bool TruncateList(List<HIS_BHYT_BLACKLIST> listData)
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

        public List<HIS_BHYT_BLACKLIST> Get(HisBhytBlacklistSO search, CommonParam param)
        {
            List<HIS_BHYT_BLACKLIST> result = new List<HIS_BHYT_BLACKLIST>();
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

        public HIS_BHYT_BLACKLIST GetById(long id, HisBhytBlacklistSO search)
        {
            HIS_BHYT_BLACKLIST result = null;
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
