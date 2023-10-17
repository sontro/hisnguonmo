using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCaroAccountBook
{
    public partial class HisCaroAccountBookDAO : EntityBase
    {
        private HisCaroAccountBookCreate CreateWorker
        {
            get
            {
                return (HisCaroAccountBookCreate)Worker.Get<HisCaroAccountBookCreate>();
            }
        }
        private HisCaroAccountBookUpdate UpdateWorker
        {
            get
            {
                return (HisCaroAccountBookUpdate)Worker.Get<HisCaroAccountBookUpdate>();
            }
        }
        private HisCaroAccountBookDelete DeleteWorker
        {
            get
            {
                return (HisCaroAccountBookDelete)Worker.Get<HisCaroAccountBookDelete>();
            }
        }
        private HisCaroAccountBookTruncate TruncateWorker
        {
            get
            {
                return (HisCaroAccountBookTruncate)Worker.Get<HisCaroAccountBookTruncate>();
            }
        }
        private HisCaroAccountBookGet GetWorker
        {
            get
            {
                return (HisCaroAccountBookGet)Worker.Get<HisCaroAccountBookGet>();
            }
        }
        private HisCaroAccountBookCheck CheckWorker
        {
            get
            {
                return (HisCaroAccountBookCheck)Worker.Get<HisCaroAccountBookCheck>();
            }
        }

        public bool Create(HIS_CARO_ACCOUNT_BOOK data)
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

        public bool CreateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
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

        public bool Update(HIS_CARO_ACCOUNT_BOOK data)
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

        public bool UpdateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
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

        public bool Delete(HIS_CARO_ACCOUNT_BOOK data)
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

        public bool DeleteList(List<HIS_CARO_ACCOUNT_BOOK> listData)
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

        public bool Truncate(HIS_CARO_ACCOUNT_BOOK data)
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

        public bool TruncateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
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

        public List<HIS_CARO_ACCOUNT_BOOK> Get(HisCaroAccountBookSO search, CommonParam param)
        {
            List<HIS_CARO_ACCOUNT_BOOK> result = new List<HIS_CARO_ACCOUNT_BOOK>();
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

        public HIS_CARO_ACCOUNT_BOOK GetById(long id, HisCaroAccountBookSO search)
        {
            HIS_CARO_ACCOUNT_BOOK result = null;
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
