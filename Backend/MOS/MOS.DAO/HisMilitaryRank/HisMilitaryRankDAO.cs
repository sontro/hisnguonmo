using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMilitaryRank
{
    public partial class HisMilitaryRankDAO : EntityBase
    {
        private HisMilitaryRankCreate CreateWorker
        {
            get
            {
                return (HisMilitaryRankCreate)Worker.Get<HisMilitaryRankCreate>();
            }
        }
        private HisMilitaryRankUpdate UpdateWorker
        {
            get
            {
                return (HisMilitaryRankUpdate)Worker.Get<HisMilitaryRankUpdate>();
            }
        }
        private HisMilitaryRankDelete DeleteWorker
        {
            get
            {
                return (HisMilitaryRankDelete)Worker.Get<HisMilitaryRankDelete>();
            }
        }
        private HisMilitaryRankTruncate TruncateWorker
        {
            get
            {
                return (HisMilitaryRankTruncate)Worker.Get<HisMilitaryRankTruncate>();
            }
        }
        private HisMilitaryRankGet GetWorker
        {
            get
            {
                return (HisMilitaryRankGet)Worker.Get<HisMilitaryRankGet>();
            }
        }
        private HisMilitaryRankCheck CheckWorker
        {
            get
            {
                return (HisMilitaryRankCheck)Worker.Get<HisMilitaryRankCheck>();
            }
        }

        public bool Create(HIS_MILITARY_RANK data)
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

        public bool CreateList(List<HIS_MILITARY_RANK> listData)
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

        public bool Update(HIS_MILITARY_RANK data)
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

        public bool UpdateList(List<HIS_MILITARY_RANK> listData)
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

        public bool Delete(HIS_MILITARY_RANK data)
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

        public bool DeleteList(List<HIS_MILITARY_RANK> listData)
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

        public bool Truncate(HIS_MILITARY_RANK data)
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

        public bool TruncateList(List<HIS_MILITARY_RANK> listData)
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

        public List<HIS_MILITARY_RANK> Get(HisMilitaryRankSO search, CommonParam param)
        {
            List<HIS_MILITARY_RANK> result = new List<HIS_MILITARY_RANK>();
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

        public HIS_MILITARY_RANK GetById(long id, HisMilitaryRankSO search)
        {
            HIS_MILITARY_RANK result = null;
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
