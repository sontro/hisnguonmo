using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFinancePeriod
{
    public partial class HisFinancePeriodDAO : EntityBase
    {
        private HisFinancePeriodCreate CreateWorker
        {
            get
            {
                return (HisFinancePeriodCreate)Worker.Get<HisFinancePeriodCreate>();
            }
        }
        private HisFinancePeriodUpdate UpdateWorker
        {
            get
            {
                return (HisFinancePeriodUpdate)Worker.Get<HisFinancePeriodUpdate>();
            }
        }
        private HisFinancePeriodDelete DeleteWorker
        {
            get
            {
                return (HisFinancePeriodDelete)Worker.Get<HisFinancePeriodDelete>();
            }
        }
        private HisFinancePeriodTruncate TruncateWorker
        {
            get
            {
                return (HisFinancePeriodTruncate)Worker.Get<HisFinancePeriodTruncate>();
            }
        }
        private HisFinancePeriodGet GetWorker
        {
            get
            {
                return (HisFinancePeriodGet)Worker.Get<HisFinancePeriodGet>();
            }
        }
        private HisFinancePeriodCheck CheckWorker
        {
            get
            {
                return (HisFinancePeriodCheck)Worker.Get<HisFinancePeriodCheck>();
            }
        }

        public bool Create(HIS_FINANCE_PERIOD data)
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

        public bool CreateList(List<HIS_FINANCE_PERIOD> listData)
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

        public bool Update(HIS_FINANCE_PERIOD data)
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

        public bool UpdateList(List<HIS_FINANCE_PERIOD> listData)
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

        public bool Delete(HIS_FINANCE_PERIOD data)
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

        public bool DeleteList(List<HIS_FINANCE_PERIOD> listData)
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

        public bool Truncate(HIS_FINANCE_PERIOD data)
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

        public bool TruncateList(List<HIS_FINANCE_PERIOD> listData)
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

        public List<HIS_FINANCE_PERIOD> Get(HisFinancePeriodSO search, CommonParam param)
        {
            List<HIS_FINANCE_PERIOD> result = new List<HIS_FINANCE_PERIOD>();
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

        public HIS_FINANCE_PERIOD GetById(long id, HisFinancePeriodSO search)
        {
            HIS_FINANCE_PERIOD result = null;
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
