using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockMety
{
    public partial class HisMediStockMetyDAO : EntityBase
    {
        private HisMediStockMetyCreate CreateWorker
        {
            get
            {
                return (HisMediStockMetyCreate)Worker.Get<HisMediStockMetyCreate>();
            }
        }
        private HisMediStockMetyUpdate UpdateWorker
        {
            get
            {
                return (HisMediStockMetyUpdate)Worker.Get<HisMediStockMetyUpdate>();
            }
        }
        private HisMediStockMetyDelete DeleteWorker
        {
            get
            {
                return (HisMediStockMetyDelete)Worker.Get<HisMediStockMetyDelete>();
            }
        }
        private HisMediStockMetyTruncate TruncateWorker
        {
            get
            {
                return (HisMediStockMetyTruncate)Worker.Get<HisMediStockMetyTruncate>();
            }
        }
        private HisMediStockMetyGet GetWorker
        {
            get
            {
                return (HisMediStockMetyGet)Worker.Get<HisMediStockMetyGet>();
            }
        }
        private HisMediStockMetyCheck CheckWorker
        {
            get
            {
                return (HisMediStockMetyCheck)Worker.Get<HisMediStockMetyCheck>();
            }
        }

        public bool Create(HIS_MEDI_STOCK_METY data)
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

        public bool CreateList(List<HIS_MEDI_STOCK_METY> listData)
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

        public bool Update(HIS_MEDI_STOCK_METY data)
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

        public bool UpdateList(List<HIS_MEDI_STOCK_METY> listData)
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

        public bool Delete(HIS_MEDI_STOCK_METY data)
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

        public bool DeleteList(List<HIS_MEDI_STOCK_METY> listData)
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

        public bool Truncate(HIS_MEDI_STOCK_METY data)
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

        public bool TruncateList(List<HIS_MEDI_STOCK_METY> listData)
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

        public List<HIS_MEDI_STOCK_METY> Get(HisMediStockMetySO search, CommonParam param)
        {
            List<HIS_MEDI_STOCK_METY> result = new List<HIS_MEDI_STOCK_METY>();
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

        public HIS_MEDI_STOCK_METY GetById(long id, HisMediStockMetySO search)
        {
            HIS_MEDI_STOCK_METY result = null;
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
