using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockPeriod
{
    public partial class HisMediStockPeriodDAO : EntityBase
    {
        private HisMediStockPeriodCreate CreateWorker
        {
            get
            {
                return (HisMediStockPeriodCreate)Worker.Get<HisMediStockPeriodCreate>();
            }
        }
        private HisMediStockPeriodUpdate UpdateWorker
        {
            get
            {
                return (HisMediStockPeriodUpdate)Worker.Get<HisMediStockPeriodUpdate>();
            }
        }
        private HisMediStockPeriodDelete DeleteWorker
        {
            get
            {
                return (HisMediStockPeriodDelete)Worker.Get<HisMediStockPeriodDelete>();
            }
        }
        private HisMediStockPeriodTruncate TruncateWorker
        {
            get
            {
                return (HisMediStockPeriodTruncate)Worker.Get<HisMediStockPeriodTruncate>();
            }
        }
        private HisMediStockPeriodGet GetWorker
        {
            get
            {
                return (HisMediStockPeriodGet)Worker.Get<HisMediStockPeriodGet>();
            }
        }
        private HisMediStockPeriodCheck CheckWorker
        {
            get
            {
                return (HisMediStockPeriodCheck)Worker.Get<HisMediStockPeriodCheck>();
            }
        }

        public bool Create(HIS_MEDI_STOCK_PERIOD data)
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

        public bool CreateList(List<HIS_MEDI_STOCK_PERIOD> listData)
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

        public bool Update(HIS_MEDI_STOCK_PERIOD data)
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

        public bool UpdateList(List<HIS_MEDI_STOCK_PERIOD> listData)
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

        public bool Delete(HIS_MEDI_STOCK_PERIOD data)
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

        public bool DeleteList(List<HIS_MEDI_STOCK_PERIOD> listData)
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

        public bool Truncate(HIS_MEDI_STOCK_PERIOD data)
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

        public bool TruncateList(List<HIS_MEDI_STOCK_PERIOD> listData)
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

        public List<HIS_MEDI_STOCK_PERIOD> Get(HisMediStockPeriodSO search, CommonParam param)
        {
            List<HIS_MEDI_STOCK_PERIOD> result = new List<HIS_MEDI_STOCK_PERIOD>();
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

        public HIS_MEDI_STOCK_PERIOD GetById(long id, HisMediStockPeriodSO search)
        {
            HIS_MEDI_STOCK_PERIOD result = null;
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
