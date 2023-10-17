using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlood
{
    public partial class HisMestPeriodBloodDAO : EntityBase
    {
        private HisMestPeriodBloodCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodBloodCreate)Worker.Get<HisMestPeriodBloodCreate>();
            }
        }
        private HisMestPeriodBloodUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodBloodUpdate)Worker.Get<HisMestPeriodBloodUpdate>();
            }
        }
        private HisMestPeriodBloodDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodBloodDelete)Worker.Get<HisMestPeriodBloodDelete>();
            }
        }
        private HisMestPeriodBloodTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodBloodTruncate)Worker.Get<HisMestPeriodBloodTruncate>();
            }
        }
        private HisMestPeriodBloodGet GetWorker
        {
            get
            {
                return (HisMestPeriodBloodGet)Worker.Get<HisMestPeriodBloodGet>();
            }
        }
        private HisMestPeriodBloodCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodBloodCheck)Worker.Get<HisMestPeriodBloodCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_BLOOD data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_BLOOD> listData)
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

        public bool Update(HIS_MEST_PERIOD_BLOOD data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_BLOOD> listData)
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

        public bool Delete(HIS_MEST_PERIOD_BLOOD data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_BLOOD> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_BLOOD data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_BLOOD> listData)
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

        public List<HIS_MEST_PERIOD_BLOOD> Get(HisMestPeriodBloodSO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_BLOOD> result = new List<HIS_MEST_PERIOD_BLOOD>();
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

        public HIS_MEST_PERIOD_BLOOD GetById(long id, HisMestPeriodBloodSO search)
        {
            HIS_MEST_PERIOD_BLOOD result = null;
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
