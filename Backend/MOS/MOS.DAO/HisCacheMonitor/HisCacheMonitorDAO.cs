using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCacheMonitor
{
    public partial class HisCacheMonitorDAO : EntityBase
    {
        private HisCacheMonitorCreate CreateWorker
        {
            get
            {
                return (HisCacheMonitorCreate)Worker.Get<HisCacheMonitorCreate>();
            }
        }
        private HisCacheMonitorUpdate UpdateWorker
        {
            get
            {
                return (HisCacheMonitorUpdate)Worker.Get<HisCacheMonitorUpdate>();
            }
        }
        private HisCacheMonitorDelete DeleteWorker
        {
            get
            {
                return (HisCacheMonitorDelete)Worker.Get<HisCacheMonitorDelete>();
            }
        }
        private HisCacheMonitorTruncate TruncateWorker
        {
            get
            {
                return (HisCacheMonitorTruncate)Worker.Get<HisCacheMonitorTruncate>();
            }
        }
        private HisCacheMonitorGet GetWorker
        {
            get
            {
                return (HisCacheMonitorGet)Worker.Get<HisCacheMonitorGet>();
            }
        }
        private HisCacheMonitorCheck CheckWorker
        {
            get
            {
                return (HisCacheMonitorCheck)Worker.Get<HisCacheMonitorCheck>();
            }
        }

        public bool Create(HIS_CACHE_MONITOR data)
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

        public bool CreateList(List<HIS_CACHE_MONITOR> listData)
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

        public bool Update(HIS_CACHE_MONITOR data)
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

        public bool UpdateList(List<HIS_CACHE_MONITOR> listData)
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

        public bool Delete(HIS_CACHE_MONITOR data)
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

        public bool DeleteList(List<HIS_CACHE_MONITOR> listData)
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

        public bool Truncate(HIS_CACHE_MONITOR data)
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

        public bool TruncateList(List<HIS_CACHE_MONITOR> listData)
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

        public List<HIS_CACHE_MONITOR> Get(HisCacheMonitorSO search, CommonParam param)
        {
            List<HIS_CACHE_MONITOR> result = new List<HIS_CACHE_MONITOR>();
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

        public HIS_CACHE_MONITOR GetById(long id, HisCacheMonitorSO search)
        {
            HIS_CACHE_MONITOR result = null;
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
