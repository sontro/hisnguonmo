using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlty
{
    public partial class HisMestPeriodBltyDAO : EntityBase
    {
        private HisMestPeriodBltyCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodBltyCreate)Worker.Get<HisMestPeriodBltyCreate>();
            }
        }
        private HisMestPeriodBltyUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodBltyUpdate)Worker.Get<HisMestPeriodBltyUpdate>();
            }
        }
        private HisMestPeriodBltyDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodBltyDelete)Worker.Get<HisMestPeriodBltyDelete>();
            }
        }
        private HisMestPeriodBltyTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodBltyTruncate)Worker.Get<HisMestPeriodBltyTruncate>();
            }
        }
        private HisMestPeriodBltyGet GetWorker
        {
            get
            {
                return (HisMestPeriodBltyGet)Worker.Get<HisMestPeriodBltyGet>();
            }
        }
        private HisMestPeriodBltyCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodBltyCheck)Worker.Get<HisMestPeriodBltyCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_BLTY data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_BLTY> listData)
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

        public bool Update(HIS_MEST_PERIOD_BLTY data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_BLTY> listData)
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

        public bool Delete(HIS_MEST_PERIOD_BLTY data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_BLTY> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_BLTY data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_BLTY> listData)
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

        public List<HIS_MEST_PERIOD_BLTY> Get(HisMestPeriodBltySO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_BLTY> result = new List<HIS_MEST_PERIOD_BLTY>();
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

        public HIS_MEST_PERIOD_BLTY GetById(long id, HisMestPeriodBltySO search)
        {
            HIS_MEST_PERIOD_BLTY result = null;
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
