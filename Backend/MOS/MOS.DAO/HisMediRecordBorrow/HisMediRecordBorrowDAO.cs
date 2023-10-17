using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediRecordBorrow
{
    public partial class HisMediRecordBorrowDAO : EntityBase
    {
        private HisMediRecordBorrowCreate CreateWorker
        {
            get
            {
                return (HisMediRecordBorrowCreate)Worker.Get<HisMediRecordBorrowCreate>();
            }
        }
        private HisMediRecordBorrowUpdate UpdateWorker
        {
            get
            {
                return (HisMediRecordBorrowUpdate)Worker.Get<HisMediRecordBorrowUpdate>();
            }
        }
        private HisMediRecordBorrowDelete DeleteWorker
        {
            get
            {
                return (HisMediRecordBorrowDelete)Worker.Get<HisMediRecordBorrowDelete>();
            }
        }
        private HisMediRecordBorrowTruncate TruncateWorker
        {
            get
            {
                return (HisMediRecordBorrowTruncate)Worker.Get<HisMediRecordBorrowTruncate>();
            }
        }
        private HisMediRecordBorrowGet GetWorker
        {
            get
            {
                return (HisMediRecordBorrowGet)Worker.Get<HisMediRecordBorrowGet>();
            }
        }
        private HisMediRecordBorrowCheck CheckWorker
        {
            get
            {
                return (HisMediRecordBorrowCheck)Worker.Get<HisMediRecordBorrowCheck>();
            }
        }

        public bool Create(HIS_MEDI_RECORD_BORROW data)
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

        public bool CreateList(List<HIS_MEDI_RECORD_BORROW> listData)
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

        public bool Update(HIS_MEDI_RECORD_BORROW data)
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

        public bool UpdateList(List<HIS_MEDI_RECORD_BORROW> listData)
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

        public bool Delete(HIS_MEDI_RECORD_BORROW data)
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

        public bool DeleteList(List<HIS_MEDI_RECORD_BORROW> listData)
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

        public bool Truncate(HIS_MEDI_RECORD_BORROW data)
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

        public bool TruncateList(List<HIS_MEDI_RECORD_BORROW> listData)
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

        public List<HIS_MEDI_RECORD_BORROW> Get(HisMediRecordBorrowSO search, CommonParam param)
        {
            List<HIS_MEDI_RECORD_BORROW> result = new List<HIS_MEDI_RECORD_BORROW>();
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

        public HIS_MEDI_RECORD_BORROW GetById(long id, HisMediRecordBorrowSO search)
        {
            HIS_MEDI_RECORD_BORROW result = null;
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
