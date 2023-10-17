using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCarerCardBorrow
{
    public partial class HisCarerCardBorrowDAO : EntityBase
    {
        private HisCarerCardBorrowCreate CreateWorker
        {
            get
            {
                return (HisCarerCardBorrowCreate)Worker.Get<HisCarerCardBorrowCreate>();
            }
        }
        private HisCarerCardBorrowUpdate UpdateWorker
        {
            get
            {
                return (HisCarerCardBorrowUpdate)Worker.Get<HisCarerCardBorrowUpdate>();
            }
        }
        private HisCarerCardBorrowDelete DeleteWorker
        {
            get
            {
                return (HisCarerCardBorrowDelete)Worker.Get<HisCarerCardBorrowDelete>();
            }
        }
        private HisCarerCardBorrowTruncate TruncateWorker
        {
            get
            {
                return (HisCarerCardBorrowTruncate)Worker.Get<HisCarerCardBorrowTruncate>();
            }
        }
        private HisCarerCardBorrowGet GetWorker
        {
            get
            {
                return (HisCarerCardBorrowGet)Worker.Get<HisCarerCardBorrowGet>();
            }
        }
        private HisCarerCardBorrowCheck CheckWorker
        {
            get
            {
                return (HisCarerCardBorrowCheck)Worker.Get<HisCarerCardBorrowCheck>();
            }
        }

        public bool Create(HIS_CARER_CARD_BORROW data)
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

        public bool CreateList(List<HIS_CARER_CARD_BORROW> listData)
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

        public bool Update(HIS_CARER_CARD_BORROW data)
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

        public bool UpdateList(List<HIS_CARER_CARD_BORROW> listData)
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

        public bool Delete(HIS_CARER_CARD_BORROW data)
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

        public bool DeleteList(List<HIS_CARER_CARD_BORROW> listData)
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

        public bool Truncate(HIS_CARER_CARD_BORROW data)
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

        public bool TruncateList(List<HIS_CARER_CARD_BORROW> listData)
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

        public List<HIS_CARER_CARD_BORROW> Get(HisCarerCardBorrowSO search, CommonParam param)
        {
            List<HIS_CARER_CARD_BORROW> result = new List<HIS_CARER_CARD_BORROW>();
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

        public HIS_CARER_CARD_BORROW GetById(long id, HisCarerCardBorrowSO search)
        {
            HIS_CARER_CARD_BORROW result = null;
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
