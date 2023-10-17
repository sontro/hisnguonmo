using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserAccountBook
{
    public partial class HisUserAccountBookDAO : EntityBase
    {
        private HisUserAccountBookCreate CreateWorker
        {
            get
            {
                return (HisUserAccountBookCreate)Worker.Get<HisUserAccountBookCreate>();
            }
        }
        private HisUserAccountBookUpdate UpdateWorker
        {
            get
            {
                return (HisUserAccountBookUpdate)Worker.Get<HisUserAccountBookUpdate>();
            }
        }
        private HisUserAccountBookDelete DeleteWorker
        {
            get
            {
                return (HisUserAccountBookDelete)Worker.Get<HisUserAccountBookDelete>();
            }
        }
        private HisUserAccountBookTruncate TruncateWorker
        {
            get
            {
                return (HisUserAccountBookTruncate)Worker.Get<HisUserAccountBookTruncate>();
            }
        }
        private HisUserAccountBookGet GetWorker
        {
            get
            {
                return (HisUserAccountBookGet)Worker.Get<HisUserAccountBookGet>();
            }
        }
        private HisUserAccountBookCheck CheckWorker
        {
            get
            {
                return (HisUserAccountBookCheck)Worker.Get<HisUserAccountBookCheck>();
            }
        }

        public bool Create(HIS_USER_ACCOUNT_BOOK data)
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

        public bool CreateList(List<HIS_USER_ACCOUNT_BOOK> listData)
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

        public bool Update(HIS_USER_ACCOUNT_BOOK data)
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

        public bool UpdateList(List<HIS_USER_ACCOUNT_BOOK> listData)
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

        public bool Delete(HIS_USER_ACCOUNT_BOOK data)
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

        public bool DeleteList(List<HIS_USER_ACCOUNT_BOOK> listData)
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

        public bool Truncate(HIS_USER_ACCOUNT_BOOK data)
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

        public bool TruncateList(List<HIS_USER_ACCOUNT_BOOK> listData)
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

        public List<HIS_USER_ACCOUNT_BOOK> Get(HisUserAccountBookSO search, CommonParam param)
        {
            List<HIS_USER_ACCOUNT_BOOK> result = new List<HIS_USER_ACCOUNT_BOOK>();
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

        public HIS_USER_ACCOUNT_BOOK GetById(long id, HisUserAccountBookSO search)
        {
            HIS_USER_ACCOUNT_BOOK result = null;
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
