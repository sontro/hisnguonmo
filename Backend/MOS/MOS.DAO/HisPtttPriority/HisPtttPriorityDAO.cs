using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttPriority
{
    public partial class HisPtttPriorityDAO : EntityBase
    {
        private HisPtttPriorityCreate CreateWorker
        {
            get
            {
                return (HisPtttPriorityCreate)Worker.Get<HisPtttPriorityCreate>();
            }
        }
        private HisPtttPriorityUpdate UpdateWorker
        {
            get
            {
                return (HisPtttPriorityUpdate)Worker.Get<HisPtttPriorityUpdate>();
            }
        }
        private HisPtttPriorityDelete DeleteWorker
        {
            get
            {
                return (HisPtttPriorityDelete)Worker.Get<HisPtttPriorityDelete>();
            }
        }
        private HisPtttPriorityTruncate TruncateWorker
        {
            get
            {
                return (HisPtttPriorityTruncate)Worker.Get<HisPtttPriorityTruncate>();
            }
        }
        private HisPtttPriorityGet GetWorker
        {
            get
            {
                return (HisPtttPriorityGet)Worker.Get<HisPtttPriorityGet>();
            }
        }
        private HisPtttPriorityCheck CheckWorker
        {
            get
            {
                return (HisPtttPriorityCheck)Worker.Get<HisPtttPriorityCheck>();
            }
        }

        public bool Create(HIS_PTTT_PRIORITY data)
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

        public bool CreateList(List<HIS_PTTT_PRIORITY> listData)
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

        public bool Update(HIS_PTTT_PRIORITY data)
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

        public bool UpdateList(List<HIS_PTTT_PRIORITY> listData)
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

        public bool Delete(HIS_PTTT_PRIORITY data)
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

        public bool DeleteList(List<HIS_PTTT_PRIORITY> listData)
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

        public bool Truncate(HIS_PTTT_PRIORITY data)
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

        public bool TruncateList(List<HIS_PTTT_PRIORITY> listData)
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

        public List<HIS_PTTT_PRIORITY> Get(HisPtttPrioritySO search, CommonParam param)
        {
            List<HIS_PTTT_PRIORITY> result = new List<HIS_PTTT_PRIORITY>();
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

        public HIS_PTTT_PRIORITY GetById(long id, HisPtttPrioritySO search)
        {
            HIS_PTTT_PRIORITY result = null;
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
