using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisPtttGroupBest
{
    public partial class HisPtttGroupBestDAO : EntityBase
    {
        private HisPtttGroupBestCreate CreateWorker
        {
            get
            {
                return (HisPtttGroupBestCreate)Worker.Get<HisPtttGroupBestCreate>();
            }
        }
        private HisPtttGroupBestUpdate UpdateWorker
        {
            get
            {
                return (HisPtttGroupBestUpdate)Worker.Get<HisPtttGroupBestUpdate>();
            }
        }
        private HisPtttGroupBestDelete DeleteWorker
        {
            get
            {
                return (HisPtttGroupBestDelete)Worker.Get<HisPtttGroupBestDelete>();
            }
        }
        private HisPtttGroupBestTruncate TruncateWorker
        {
            get
            {
                return (HisPtttGroupBestTruncate)Worker.Get<HisPtttGroupBestTruncate>();
            }
        }
        private HisPtttGroupBestGet GetWorker
        {
            get
            {
                return (HisPtttGroupBestGet)Worker.Get<HisPtttGroupBestGet>();
            }
        }
        private HisPtttGroupBestCheck CheckWorker
        {
            get
            {
                return (HisPtttGroupBestCheck)Worker.Get<HisPtttGroupBestCheck>();
            }
        }

        public bool Create(HIS_PTTT_GROUP_BEST data)
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

        public bool CreateList(List<HIS_PTTT_GROUP_BEST> listData)
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

        public bool Update(HIS_PTTT_GROUP_BEST data)
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

        public bool UpdateList(List<HIS_PTTT_GROUP_BEST> listData)
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

        public bool Delete(HIS_PTTT_GROUP_BEST data)
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

        public bool DeleteList(List<HIS_PTTT_GROUP_BEST> listData)
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

        public bool Truncate(HIS_PTTT_GROUP_BEST data)
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

        public bool TruncateList(List<HIS_PTTT_GROUP_BEST> listData)
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

        public List<HIS_PTTT_GROUP_BEST> Get(HisPtttGroupBestSO search, CommonParam param)
        {
            List<HIS_PTTT_GROUP_BEST> result = new List<HIS_PTTT_GROUP_BEST>();
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

        public HIS_PTTT_GROUP_BEST GetById(long id, HisPtttGroupBestSO search)
        {
            HIS_PTTT_GROUP_BEST result = null;
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
