using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsActivityLog
{
    public partial class AcsActivityLogDAO : EntityBase
    {
        private AcsActivityLogCreate CreateWorker
        {
            get
            {
                return (AcsActivityLogCreate)Worker.Get<AcsActivityLogCreate>();
            }
        }
        private AcsActivityLogUpdate UpdateWorker
        {
            get
            {
                return (AcsActivityLogUpdate)Worker.Get<AcsActivityLogUpdate>();
            }
        }
        private AcsActivityLogDelete DeleteWorker
        {
            get
            {
                return (AcsActivityLogDelete)Worker.Get<AcsActivityLogDelete>();
            }
        }
        private AcsActivityLogTruncate TruncateWorker
        {
            get
            {
                return (AcsActivityLogTruncate)Worker.Get<AcsActivityLogTruncate>();
            }
        }
        private AcsActivityLogGet GetWorker
        {
            get
            {
                return (AcsActivityLogGet)Worker.Get<AcsActivityLogGet>();
            }
        }
        private AcsActivityLogCheck CheckWorker
        {
            get
            {
                return (AcsActivityLogCheck)Worker.Get<AcsActivityLogCheck>();
            }
        }

        public bool Create(ACS_ACTIVITY_LOG data)
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

        public bool CreateList(List<ACS_ACTIVITY_LOG> listData)
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

        public bool Update(ACS_ACTIVITY_LOG data)
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

        public bool UpdateList(List<ACS_ACTIVITY_LOG> listData)
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

        public bool Delete(ACS_ACTIVITY_LOG data)
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

        public bool DeleteList(List<ACS_ACTIVITY_LOG> listData)
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

        public bool Truncate(ACS_ACTIVITY_LOG data)
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

        public bool TruncateList(List<ACS_ACTIVITY_LOG> listData)
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

        public List<ACS_ACTIVITY_LOG> Get(AcsActivityLogSO search, CommonParam param)
        {
            List<ACS_ACTIVITY_LOG> result = new List<ACS_ACTIVITY_LOG>();
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

        public ACS_ACTIVITY_LOG GetById(long id, AcsActivityLogSO search)
        {
            ACS_ACTIVITY_LOG result = null;
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
