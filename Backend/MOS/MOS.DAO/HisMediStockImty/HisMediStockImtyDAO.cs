using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockImty
{
    public partial class HisMediStockImtyDAO : EntityBase
    {
        private HisMediStockImtyCreate CreateWorker
        {
            get
            {
                return (HisMediStockImtyCreate)Worker.Get<HisMediStockImtyCreate>();
            }
        }
        private HisMediStockImtyUpdate UpdateWorker
        {
            get
            {
                return (HisMediStockImtyUpdate)Worker.Get<HisMediStockImtyUpdate>();
            }
        }
        private HisMediStockImtyDelete DeleteWorker
        {
            get
            {
                return (HisMediStockImtyDelete)Worker.Get<HisMediStockImtyDelete>();
            }
        }
        private HisMediStockImtyTruncate TruncateWorker
        {
            get
            {
                return (HisMediStockImtyTruncate)Worker.Get<HisMediStockImtyTruncate>();
            }
        }
        private HisMediStockImtyGet GetWorker
        {
            get
            {
                return (HisMediStockImtyGet)Worker.Get<HisMediStockImtyGet>();
            }
        }
        private HisMediStockImtyCheck CheckWorker
        {
            get
            {
                return (HisMediStockImtyCheck)Worker.Get<HisMediStockImtyCheck>();
            }
        }

        public bool Create(HIS_MEDI_STOCK_IMTY data)
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

        public bool CreateList(List<HIS_MEDI_STOCK_IMTY> listData)
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

        public bool Update(HIS_MEDI_STOCK_IMTY data)
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

        public bool UpdateList(List<HIS_MEDI_STOCK_IMTY> listData)
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

        public bool Delete(HIS_MEDI_STOCK_IMTY data)
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

        public bool DeleteList(List<HIS_MEDI_STOCK_IMTY> listData)
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

        public bool Truncate(HIS_MEDI_STOCK_IMTY data)
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

        public bool TruncateList(List<HIS_MEDI_STOCK_IMTY> listData)
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

        public List<HIS_MEDI_STOCK_IMTY> Get(HisMediStockImtySO search, CommonParam param)
        {
            List<HIS_MEDI_STOCK_IMTY> result = new List<HIS_MEDI_STOCK_IMTY>();
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

        public HIS_MEDI_STOCK_IMTY GetById(long id, HisMediStockImtySO search)
        {
            HIS_MEDI_STOCK_IMTY result = null;
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
