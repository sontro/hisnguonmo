using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMaty
{
    public partial class HisMestPeriodMatyDAO : EntityBase
    {
        private HisMestPeriodMatyCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodMatyCreate)Worker.Get<HisMestPeriodMatyCreate>();
            }
        }
        private HisMestPeriodMatyUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodMatyUpdate)Worker.Get<HisMestPeriodMatyUpdate>();
            }
        }
        private HisMestPeriodMatyDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodMatyDelete)Worker.Get<HisMestPeriodMatyDelete>();
            }
        }
        private HisMestPeriodMatyTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodMatyTruncate)Worker.Get<HisMestPeriodMatyTruncate>();
            }
        }
        private HisMestPeriodMatyGet GetWorker
        {
            get
            {
                return (HisMestPeriodMatyGet)Worker.Get<HisMestPeriodMatyGet>();
            }
        }
        private HisMestPeriodMatyCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodMatyCheck)Worker.Get<HisMestPeriodMatyCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_MATY data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_MATY> listData)
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

        public bool Update(HIS_MEST_PERIOD_MATY data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_MATY> listData)
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

        public bool Delete(HIS_MEST_PERIOD_MATY data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_MATY> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_MATY data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_MATY> listData)
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

        public List<HIS_MEST_PERIOD_MATY> Get(HisMestPeriodMatySO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_MATY> result = new List<HIS_MEST_PERIOD_MATY>();
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

        public HIS_MEST_PERIOD_MATY GetById(long id, HisMestPeriodMatySO search)
        {
            HIS_MEST_PERIOD_MATY result = null;
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
