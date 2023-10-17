using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReason
{
    public partial class HisDepositReasonDAO : EntityBase
    {
        private HisDepositReasonCreate CreateWorker
        {
            get
            {
                return (HisDepositReasonCreate)Worker.Get<HisDepositReasonCreate>();
            }
        }
        private HisDepositReasonUpdate UpdateWorker
        {
            get
            {
                return (HisDepositReasonUpdate)Worker.Get<HisDepositReasonUpdate>();
            }
        }
        private HisDepositReasonDelete DeleteWorker
        {
            get
            {
                return (HisDepositReasonDelete)Worker.Get<HisDepositReasonDelete>();
            }
        }
        private HisDepositReasonTruncate TruncateWorker
        {
            get
            {
                return (HisDepositReasonTruncate)Worker.Get<HisDepositReasonTruncate>();
            }
        }
        private HisDepositReasonGet GetWorker
        {
            get
            {
                return (HisDepositReasonGet)Worker.Get<HisDepositReasonGet>();
            }
        }
        private HisDepositReasonCheck CheckWorker
        {
            get
            {
                return (HisDepositReasonCheck)Worker.Get<HisDepositReasonCheck>();
            }
        }

        public bool Create(HIS_DEPOSIT_REASON data)
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

        public bool CreateList(List<HIS_DEPOSIT_REASON> listData)
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

        public bool Update(HIS_DEPOSIT_REASON data)
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

        public bool UpdateList(List<HIS_DEPOSIT_REASON> listData)
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

        public bool Delete(HIS_DEPOSIT_REASON data)
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

        public bool DeleteList(List<HIS_DEPOSIT_REASON> listData)
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

        public bool Truncate(HIS_DEPOSIT_REASON data)
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

        public bool TruncateList(List<HIS_DEPOSIT_REASON> listData)
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

        public List<HIS_DEPOSIT_REASON> Get(HisDepositReasonSO search, CommonParam param)
        {
            List<HIS_DEPOSIT_REASON> result = new List<HIS_DEPOSIT_REASON>();
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

        public HIS_DEPOSIT_REASON GetById(long id, HisDepositReasonSO search)
        {
            HIS_DEPOSIT_REASON result = null;
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
