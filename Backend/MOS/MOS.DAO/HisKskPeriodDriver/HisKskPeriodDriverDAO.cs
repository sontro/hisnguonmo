using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisKskPeriodDriver
{
    public partial class HisKskPeriodDriverDAO : EntityBase
    {
        private HisKskPeriodDriverCreate CreateWorker
        {
            get
            {
                return (HisKskPeriodDriverCreate)Worker.Get<HisKskPeriodDriverCreate>();
            }
        }
        private HisKskPeriodDriverUpdate UpdateWorker
        {
            get
            {
                return (HisKskPeriodDriverUpdate)Worker.Get<HisKskPeriodDriverUpdate>();
            }
        }
        private HisKskPeriodDriverDelete DeleteWorker
        {
            get
            {
                return (HisKskPeriodDriverDelete)Worker.Get<HisKskPeriodDriverDelete>();
            }
        }
        private HisKskPeriodDriverTruncate TruncateWorker
        {
            get
            {
                return (HisKskPeriodDriverTruncate)Worker.Get<HisKskPeriodDriverTruncate>();
            }
        }
        private HisKskPeriodDriverGet GetWorker
        {
            get
            {
                return (HisKskPeriodDriverGet)Worker.Get<HisKskPeriodDriverGet>();
            }
        }
        private HisKskPeriodDriverCheck CheckWorker
        {
            get
            {
                return (HisKskPeriodDriverCheck)Worker.Get<HisKskPeriodDriverCheck>();
            }
        }

        public bool Create(HIS_KSK_PERIOD_DRIVER data)
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

        public bool CreateList(List<HIS_KSK_PERIOD_DRIVER> listData)
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

        public bool Update(HIS_KSK_PERIOD_DRIVER data)
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

        public bool UpdateList(List<HIS_KSK_PERIOD_DRIVER> listData)
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

        public bool Delete(HIS_KSK_PERIOD_DRIVER data)
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

        public bool DeleteList(List<HIS_KSK_PERIOD_DRIVER> listData)
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

        public bool Truncate(HIS_KSK_PERIOD_DRIVER data)
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

        public bool TruncateList(List<HIS_KSK_PERIOD_DRIVER> listData)
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

        public List<HIS_KSK_PERIOD_DRIVER> Get(HisKskPeriodDriverSO search, CommonParam param)
        {
            List<HIS_KSK_PERIOD_DRIVER> result = new List<HIS_KSK_PERIOD_DRIVER>();
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

        public HIS_KSK_PERIOD_DRIVER GetById(long id, HisKskPeriodDriverSO search)
        {
            HIS_KSK_PERIOD_DRIVER result = null;
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
