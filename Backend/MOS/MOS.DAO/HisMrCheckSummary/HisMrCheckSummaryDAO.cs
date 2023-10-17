using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckSummary
{
    public partial class HisMrCheckSummaryDAO : EntityBase
    {
        private HisMrCheckSummaryCreate CreateWorker
        {
            get
            {
                return (HisMrCheckSummaryCreate)Worker.Get<HisMrCheckSummaryCreate>();
            }
        }
        private HisMrCheckSummaryUpdate UpdateWorker
        {
            get
            {
                return (HisMrCheckSummaryUpdate)Worker.Get<HisMrCheckSummaryUpdate>();
            }
        }
        private HisMrCheckSummaryDelete DeleteWorker
        {
            get
            {
                return (HisMrCheckSummaryDelete)Worker.Get<HisMrCheckSummaryDelete>();
            }
        }
        private HisMrCheckSummaryTruncate TruncateWorker
        {
            get
            {
                return (HisMrCheckSummaryTruncate)Worker.Get<HisMrCheckSummaryTruncate>();
            }
        }
        private HisMrCheckSummaryGet GetWorker
        {
            get
            {
                return (HisMrCheckSummaryGet)Worker.Get<HisMrCheckSummaryGet>();
            }
        }
        private HisMrCheckSummaryCheck CheckWorker
        {
            get
            {
                return (HisMrCheckSummaryCheck)Worker.Get<HisMrCheckSummaryCheck>();
            }
        }

        public bool Create(HIS_MR_CHECK_SUMMARY data)
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

        public bool CreateList(List<HIS_MR_CHECK_SUMMARY> listData)
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

        public bool Update(HIS_MR_CHECK_SUMMARY data)
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

        public bool UpdateList(List<HIS_MR_CHECK_SUMMARY> listData)
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

        public bool Delete(HIS_MR_CHECK_SUMMARY data)
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

        public bool DeleteList(List<HIS_MR_CHECK_SUMMARY> listData)
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

        public bool Truncate(HIS_MR_CHECK_SUMMARY data)
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

        public bool TruncateList(List<HIS_MR_CHECK_SUMMARY> listData)
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

        public List<HIS_MR_CHECK_SUMMARY> Get(HisMrCheckSummarySO search, CommonParam param)
        {
            List<HIS_MR_CHECK_SUMMARY> result = new List<HIS_MR_CHECK_SUMMARY>();
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

        public HIS_MR_CHECK_SUMMARY GetById(long id, HisMrCheckSummarySO search)
        {
            HIS_MR_CHECK_SUMMARY result = null;
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
