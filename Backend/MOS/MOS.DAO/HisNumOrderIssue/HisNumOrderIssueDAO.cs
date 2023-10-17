using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderIssue
{
    public partial class HisNumOrderIssueDAO : EntityBase
    {
        private HisNumOrderIssueCreate CreateWorker
        {
            get
            {
                return (HisNumOrderIssueCreate)Worker.Get<HisNumOrderIssueCreate>();
            }
        }
        private HisNumOrderIssueUpdate UpdateWorker
        {
            get
            {
                return (HisNumOrderIssueUpdate)Worker.Get<HisNumOrderIssueUpdate>();
            }
        }
        private HisNumOrderIssueDelete DeleteWorker
        {
            get
            {
                return (HisNumOrderIssueDelete)Worker.Get<HisNumOrderIssueDelete>();
            }
        }
        private HisNumOrderIssueTruncate TruncateWorker
        {
            get
            {
                return (HisNumOrderIssueTruncate)Worker.Get<HisNumOrderIssueTruncate>();
            }
        }
        private HisNumOrderIssueGet GetWorker
        {
            get
            {
                return (HisNumOrderIssueGet)Worker.Get<HisNumOrderIssueGet>();
            }
        }
        private HisNumOrderIssueCheck CheckWorker
        {
            get
            {
                return (HisNumOrderIssueCheck)Worker.Get<HisNumOrderIssueCheck>();
            }
        }

        public bool Create(HIS_NUM_ORDER_ISSUE data)
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

        public bool CreateList(List<HIS_NUM_ORDER_ISSUE> listData)
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

        public bool Update(HIS_NUM_ORDER_ISSUE data)
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

        public bool UpdateList(List<HIS_NUM_ORDER_ISSUE> listData)
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

        public bool Delete(HIS_NUM_ORDER_ISSUE data)
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

        public bool DeleteList(List<HIS_NUM_ORDER_ISSUE> listData)
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

        public bool Truncate(HIS_NUM_ORDER_ISSUE data)
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

        public bool TruncateList(List<HIS_NUM_ORDER_ISSUE> listData)
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

        public List<HIS_NUM_ORDER_ISSUE> Get(HisNumOrderIssueSO search, CommonParam param)
        {
            List<HIS_NUM_ORDER_ISSUE> result = new List<HIS_NUM_ORDER_ISSUE>();
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

        public HIS_NUM_ORDER_ISSUE GetById(long id, HisNumOrderIssueSO search)
        {
            HIS_NUM_ORDER_ISSUE result = null;
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
