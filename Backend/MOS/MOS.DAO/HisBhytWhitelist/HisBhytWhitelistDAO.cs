using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytWhitelist
{
    public partial class HisBhytWhitelistDAO : EntityBase
    {
        private HisBhytWhitelistCreate CreateWorker
        {
            get
            {
                return (HisBhytWhitelistCreate)Worker.Get<HisBhytWhitelistCreate>();
            }
        }
        private HisBhytWhitelistUpdate UpdateWorker
        {
            get
            {
                return (HisBhytWhitelistUpdate)Worker.Get<HisBhytWhitelistUpdate>();
            }
        }
        private HisBhytWhitelistDelete DeleteWorker
        {
            get
            {
                return (HisBhytWhitelistDelete)Worker.Get<HisBhytWhitelistDelete>();
            }
        }
        private HisBhytWhitelistTruncate TruncateWorker
        {
            get
            {
                return (HisBhytWhitelistTruncate)Worker.Get<HisBhytWhitelistTruncate>();
            }
        }
        private HisBhytWhitelistGet GetWorker
        {
            get
            {
                return (HisBhytWhitelistGet)Worker.Get<HisBhytWhitelistGet>();
            }
        }
        private HisBhytWhitelistCheck CheckWorker
        {
            get
            {
                return (HisBhytWhitelistCheck)Worker.Get<HisBhytWhitelistCheck>();
            }
        }

        public bool Create(HIS_BHYT_WHITELIST data)
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

        public bool CreateList(List<HIS_BHYT_WHITELIST> listData)
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

        public bool Update(HIS_BHYT_WHITELIST data)
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

        public bool UpdateList(List<HIS_BHYT_WHITELIST> listData)
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

        public bool Delete(HIS_BHYT_WHITELIST data)
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

        public bool DeleteList(List<HIS_BHYT_WHITELIST> listData)
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

        public bool Truncate(HIS_BHYT_WHITELIST data)
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

        public bool TruncateList(List<HIS_BHYT_WHITELIST> listData)
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

        public List<HIS_BHYT_WHITELIST> Get(HisBhytWhitelistSO search, CommonParam param)
        {
            List<HIS_BHYT_WHITELIST> result = new List<HIS_BHYT_WHITELIST>();
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

        public HIS_BHYT_WHITELIST GetById(long id, HisBhytWhitelistSO search)
        {
            HIS_BHYT_WHITELIST result = null;
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
