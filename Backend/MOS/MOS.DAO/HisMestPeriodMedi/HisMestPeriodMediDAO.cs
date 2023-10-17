using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMedi
{
    public partial class HisMestPeriodMediDAO : EntityBase
    {
        private HisMestPeriodMediCreate CreateWorker
        {
            get
            {
                return (HisMestPeriodMediCreate)Worker.Get<HisMestPeriodMediCreate>();
            }
        }
        private HisMestPeriodMediUpdate UpdateWorker
        {
            get
            {
                return (HisMestPeriodMediUpdate)Worker.Get<HisMestPeriodMediUpdate>();
            }
        }
        private HisMestPeriodMediDelete DeleteWorker
        {
            get
            {
                return (HisMestPeriodMediDelete)Worker.Get<HisMestPeriodMediDelete>();
            }
        }
        private HisMestPeriodMediTruncate TruncateWorker
        {
            get
            {
                return (HisMestPeriodMediTruncate)Worker.Get<HisMestPeriodMediTruncate>();
            }
        }
        private HisMestPeriodMediGet GetWorker
        {
            get
            {
                return (HisMestPeriodMediGet)Worker.Get<HisMestPeriodMediGet>();
            }
        }
        private HisMestPeriodMediCheck CheckWorker
        {
            get
            {
                return (HisMestPeriodMediCheck)Worker.Get<HisMestPeriodMediCheck>();
            }
        }

        public bool Create(HIS_MEST_PERIOD_MEDI data)
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

        public bool CreateList(List<HIS_MEST_PERIOD_MEDI> listData)
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

        public bool Update(HIS_MEST_PERIOD_MEDI data)
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

        public bool UpdateList(List<HIS_MEST_PERIOD_MEDI> listData)
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

        public bool Delete(HIS_MEST_PERIOD_MEDI data)
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

        public bool DeleteList(List<HIS_MEST_PERIOD_MEDI> listData)
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

        public bool Truncate(HIS_MEST_PERIOD_MEDI data)
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

        public bool TruncateList(List<HIS_MEST_PERIOD_MEDI> listData)
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

        public List<HIS_MEST_PERIOD_MEDI> Get(HisMestPeriodMediSO search, CommonParam param)
        {
            List<HIS_MEST_PERIOD_MEDI> result = new List<HIS_MEST_PERIOD_MEDI>();
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

        public HIS_MEST_PERIOD_MEDI GetById(long id, HisMestPeriodMediSO search)
        {
            HIS_MEST_PERIOD_MEDI result = null;
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
