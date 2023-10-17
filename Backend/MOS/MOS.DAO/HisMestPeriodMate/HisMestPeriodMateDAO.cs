using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMate
{
    public partial class HisMestPeriodMateDAO : EntityBase
    {
        private HisMestPeriodMateCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodMateCreate)Worker.Get<HisMestPeriodMateCreate>();
            }
        }
        private HisMestPeriodMateUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodMateUpdate)Worker.Get<HisMestPeriodMateUpdate>();
            }
        }
        private HisMestPeriodMateDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodMateDelete)Worker.Get<HisMestPeriodMateDelete>();
            }
        }
        private HisMestPeriodMateTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodMateTruncate)Worker.Get<HisMestPeriodMateTruncate>();
            }
        }
        private HisMestPeriodMateGet GetWorker
        {
            get
            {
                return (HisMestPeriodMateGet)Worker.Get<HisMestPeriodMateGet>();
            }
        }
        private HisMestPeriodMateCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodMateCheck)Worker.Get<HisMestPeriodMateCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_MATE data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_MATE> listData)
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

        public bool Update(HIS_MEST_PERIOD_MATE data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_MATE> listData)
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

        public bool Delete(HIS_MEST_PERIOD_MATE data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_MATE> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_MATE data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_MATE> listData)
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

        public List<HIS_MEST_PERIOD_MATE> Get(HisMestPeriodMateSO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_MATE> result = new List<HIS_MEST_PERIOD_MATE>();
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

        public HIS_MEST_PERIOD_MATE GetById(long id, HisMestPeriodMateSO search)
        {
            HIS_MEST_PERIOD_MATE result = null;
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
