using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMety
{
    public partial class HisMestPeriodMetyDAO : EntityBase
    {
        private HisMestPeriodMetyCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodMetyCreate)Worker.Get<HisMestPeriodMetyCreate>();
            }
        }
        private HisMestPeriodMetyUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodMetyUpdate)Worker.Get<HisMestPeriodMetyUpdate>();
            }
        }
        private HisMestPeriodMetyDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodMetyDelete)Worker.Get<HisMestPeriodMetyDelete>();
            }
        }
        private HisMestPeriodMetyTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodMetyTruncate)Worker.Get<HisMestPeriodMetyTruncate>();
            }
        }
        private HisMestPeriodMetyGet GetWorker
        {
            get
            {
                return (HisMestPeriodMetyGet)Worker.Get<HisMestPeriodMetyGet>();
            }
        }
        private HisMestPeriodMetyCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodMetyCheck)Worker.Get<HisMestPeriodMetyCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_METY data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_METY> listData)
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

        public bool Update(HIS_MEST_PERIOD_METY data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_METY> listData)
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

        public bool Delete(HIS_MEST_PERIOD_METY data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_METY> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_METY data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_METY> listData)
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

        public List<HIS_MEST_PERIOD_METY> Get(HisMestPeriodMetySO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_METY> result = new List<HIS_MEST_PERIOD_METY>();
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

        public HIS_MEST_PERIOD_METY GetById(long id, HisMestPeriodMetySO search)
        {
            HIS_MEST_PERIOD_METY result = null;
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
