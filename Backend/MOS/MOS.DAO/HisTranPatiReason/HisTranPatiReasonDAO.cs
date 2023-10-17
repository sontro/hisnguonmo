using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTranPatiReason
{
    public partial class HisTranPatiReasonDAO : EntityBase
    {
        private HisTranPatiReasonCreate CreateWorker
        {
            get
            {
                return (HisTranPatiReasonCreate)Worker.Get<HisTranPatiReasonCreate>();
            }
        }
        private HisTranPatiReasonUpdate UpdateWorker
        {
            get
            {
                return (HisTranPatiReasonUpdate)Worker.Get<HisTranPatiReasonUpdate>();
            }
        }
        private HisTranPatiReasonDelete DeleteWorker
        {
            get
            {
                return (HisTranPatiReasonDelete)Worker.Get<HisTranPatiReasonDelete>();
            }
        }
        private HisTranPatiReasonTruncate TruncateWorker
        {
            get
            {
                return (HisTranPatiReasonTruncate)Worker.Get<HisTranPatiReasonTruncate>();
            }
        }
        private HisTranPatiReasonGet GetWorker
        {
            get
            {
                return (HisTranPatiReasonGet)Worker.Get<HisTranPatiReasonGet>();
            }
        }
        private HisTranPatiReasonCheck CheckWorker
        {
            get
            {
                return (HisTranPatiReasonCheck)Worker.Get<HisTranPatiReasonCheck>();
            }
        }

        public bool Create(HIS_TRAN_PATI_REASON data)
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

        public bool CreateList(List<HIS_TRAN_PATI_REASON> listData)
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

        public bool Update(HIS_TRAN_PATI_REASON data)
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

        public bool UpdateList(List<HIS_TRAN_PATI_REASON> listData)
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

        public bool Delete(HIS_TRAN_PATI_REASON data)
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

        public bool DeleteList(List<HIS_TRAN_PATI_REASON> listData)
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

        public bool Truncate(HIS_TRAN_PATI_REASON data)
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

        public bool TruncateList(List<HIS_TRAN_PATI_REASON> listData)
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

        public List<HIS_TRAN_PATI_REASON> Get(HisTranPatiReasonSO search, CommonParam param)
        {
            List<HIS_TRAN_PATI_REASON> result = new List<HIS_TRAN_PATI_REASON>();
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

        public HIS_TRAN_PATI_REASON GetById(long id, HisTranPatiReasonSO search)
        {
            HIS_TRAN_PATI_REASON result = null;
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
