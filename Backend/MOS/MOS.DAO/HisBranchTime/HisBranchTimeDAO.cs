using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBranchTime
{
    public partial class HisBranchTimeDAO : EntityBase
    {
        private HisBranchTimeCreate CreateWorker
        {
            get
            {
                return (HisBranchTimeCreate)Worker.Get<HisBranchTimeCreate>();
            }
        }
        private HisBranchTimeUpdate UpdateWorker
        {
            get
            {
                return (HisBranchTimeUpdate)Worker.Get<HisBranchTimeUpdate>();
            }
        }
        private HisBranchTimeDelete DeleteWorker
        {
            get
            {
                return (HisBranchTimeDelete)Worker.Get<HisBranchTimeDelete>();
            }
        }
        private HisBranchTimeTruncate TruncateWorker
        {
            get
            {
                return (HisBranchTimeTruncate)Worker.Get<HisBranchTimeTruncate>();
            }
        }
        private HisBranchTimeGet GetWorker
        {
            get
            {
                return (HisBranchTimeGet)Worker.Get<HisBranchTimeGet>();
            }
        }
        private HisBranchTimeCheck CheckWorker
        {
            get
            {
                return (HisBranchTimeCheck)Worker.Get<HisBranchTimeCheck>();
            }
        }

        public bool Create(HIS_BRANCH_TIME data)
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

        public bool CreateList(List<HIS_BRANCH_TIME> listData)
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

        public bool Update(HIS_BRANCH_TIME data)
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

        public bool UpdateList(List<HIS_BRANCH_TIME> listData)
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

        public bool Delete(HIS_BRANCH_TIME data)
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

        public bool DeleteList(List<HIS_BRANCH_TIME> listData)
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

        public bool Truncate(HIS_BRANCH_TIME data)
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

        public bool TruncateList(List<HIS_BRANCH_TIME> listData)
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

        public List<HIS_BRANCH_TIME> Get(HisBranchTimeSO search, CommonParam param)
        {
            List<HIS_BRANCH_TIME> result = new List<HIS_BRANCH_TIME>();
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

        public HIS_BRANCH_TIME GetById(long id, HisBranchTimeSO search)
        {
            HIS_BRANCH_TIME result = null;
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
