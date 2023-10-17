using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRepayReason
{
    public partial class HisRepayReasonDAO : EntityBase
    {
        private HisRepayReasonCreate CreateWorker
        {
            get
            {
                return (HisRepayReasonCreate)Worker.Get<HisRepayReasonCreate>();
            }
        }
        private HisRepayReasonUpdate UpdateWorker
        {
            get
            {
                return (HisRepayReasonUpdate)Worker.Get<HisRepayReasonUpdate>();
            }
        }
        private HisRepayReasonDelete DeleteWorker
        {
            get
            {
                return (HisRepayReasonDelete)Worker.Get<HisRepayReasonDelete>();
            }
        }
        private HisRepayReasonTruncate TruncateWorker
        {
            get
            {
                return (HisRepayReasonTruncate)Worker.Get<HisRepayReasonTruncate>();
            }
        }
        private HisRepayReasonGet GetWorker
        {
            get
            {
                return (HisRepayReasonGet)Worker.Get<HisRepayReasonGet>();
            }
        }
        private HisRepayReasonCheck CheckWorker
        {
            get
            {
                return (HisRepayReasonCheck)Worker.Get<HisRepayReasonCheck>();
            }
        }

        public bool Create(HIS_REPAY_REASON data)
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

        public bool CreateList(List<HIS_REPAY_REASON> listData)
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

        public bool Update(HIS_REPAY_REASON data)
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

        public bool UpdateList(List<HIS_REPAY_REASON> listData)
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

        public bool Delete(HIS_REPAY_REASON data)
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

        public bool DeleteList(List<HIS_REPAY_REASON> listData)
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

        public bool Truncate(HIS_REPAY_REASON data)
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

        public bool TruncateList(List<HIS_REPAY_REASON> listData)
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

        public List<HIS_REPAY_REASON> Get(HisRepayReasonSO search, CommonParam param)
        {
            List<HIS_REPAY_REASON> result = new List<HIS_REPAY_REASON>();
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

        public HIS_REPAY_REASON GetById(long id, HisRepayReasonSO search)
        {
            HIS_REPAY_REASON result = null;
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
