using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUnlimitReason
{
    public partial class HisUnlimitReasonDAO : EntityBase
    {
        private HisUnlimitReasonCreate CreateWorker
        {
            get
            {
                return (HisUnlimitReasonCreate)Worker.Get<HisUnlimitReasonCreate>();
            }
        }
        private HisUnlimitReasonUpdate UpdateWorker
        {
            get
            {
                return (HisUnlimitReasonUpdate)Worker.Get<HisUnlimitReasonUpdate>();
            }
        }
        private HisUnlimitReasonDelete DeleteWorker
        {
            get
            {
                return (HisUnlimitReasonDelete)Worker.Get<HisUnlimitReasonDelete>();
            }
        }
        private HisUnlimitReasonTruncate TruncateWorker
        {
            get
            {
                return (HisUnlimitReasonTruncate)Worker.Get<HisUnlimitReasonTruncate>();
            }
        }
        private HisUnlimitReasonGet GetWorker
        {
            get
            {
                return (HisUnlimitReasonGet)Worker.Get<HisUnlimitReasonGet>();
            }
        }
        private HisUnlimitReasonCheck CheckWorker
        {
            get
            {
                return (HisUnlimitReasonCheck)Worker.Get<HisUnlimitReasonCheck>();
            }
        }

        public bool Create(HIS_UNLIMIT_REASON data)
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

        public bool CreateList(List<HIS_UNLIMIT_REASON> listData)
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

        public bool Update(HIS_UNLIMIT_REASON data)
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

        public bool UpdateList(List<HIS_UNLIMIT_REASON> listData)
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

        public bool Delete(HIS_UNLIMIT_REASON data)
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

        public bool DeleteList(List<HIS_UNLIMIT_REASON> listData)
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

        public bool Truncate(HIS_UNLIMIT_REASON data)
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

        public bool TruncateList(List<HIS_UNLIMIT_REASON> listData)
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

        public List<HIS_UNLIMIT_REASON> Get(HisUnlimitReasonSO search, CommonParam param)
        {
            List<HIS_UNLIMIT_REASON> result = new List<HIS_UNLIMIT_REASON>();
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

        public HIS_UNLIMIT_REASON GetById(long id, HisUnlimitReasonSO search)
        {
            HIS_UNLIMIT_REASON result = null;
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
