using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentBorrow
{
    public partial class HisTreatmentBorrowDAO : EntityBase
    {
        private HisTreatmentBorrowCreate CreateWorker
        {
            get
            {
                return (HisTreatmentBorrowCreate)Worker.Get<HisTreatmentBorrowCreate>();
            }
        }
        private HisTreatmentBorrowUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentBorrowUpdate)Worker.Get<HisTreatmentBorrowUpdate>();
            }
        }
        private HisTreatmentBorrowDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentBorrowDelete)Worker.Get<HisTreatmentBorrowDelete>();
            }
        }
        private HisTreatmentBorrowTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentBorrowTruncate)Worker.Get<HisTreatmentBorrowTruncate>();
            }
        }
        private HisTreatmentBorrowGet GetWorker
        {
            get
            {
                return (HisTreatmentBorrowGet)Worker.Get<HisTreatmentBorrowGet>();
            }
        }
        private HisTreatmentBorrowCheck CheckWorker
        {
            get
            {
                return (HisTreatmentBorrowCheck)Worker.Get<HisTreatmentBorrowCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_BORROW data)
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

        public bool CreateList(List<HIS_TREATMENT_BORROW> listData)
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

        public bool Update(HIS_TREATMENT_BORROW data)
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

        public bool UpdateList(List<HIS_TREATMENT_BORROW> listData)
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

        public bool Delete(HIS_TREATMENT_BORROW data)
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

        public bool DeleteList(List<HIS_TREATMENT_BORROW> listData)
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

        public bool Truncate(HIS_TREATMENT_BORROW data)
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

        public bool TruncateList(List<HIS_TREATMENT_BORROW> listData)
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

        public List<HIS_TREATMENT_BORROW> Get(HisTreatmentBorrowSO search, CommonParam param)
        {
            List<HIS_TREATMENT_BORROW> result = new List<HIS_TREATMENT_BORROW>();
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

        public HIS_TREATMENT_BORROW GetById(long id, HisTreatmentBorrowSO search)
        {
            HIS_TREATMENT_BORROW result = null;
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
