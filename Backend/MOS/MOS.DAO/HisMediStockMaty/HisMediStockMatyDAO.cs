using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockMaty
{
    public partial class HisMediStockMatyDAO : EntityBase
    {
        private HisMediStockMatyCreate CreateWorker
        {
            get
            {
                return (HisMediStockMatyCreate)Worker.Get<HisMediStockMatyCreate>();
            }
        }
        private HisMediStockMatyUpdate UpdateWorker
        {
            get
            {
                return (HisMediStockMatyUpdate)Worker.Get<HisMediStockMatyUpdate>();
            }
        }
        private HisMediStockMatyDelete DeleteWorker
        {
            get
            {
                return (HisMediStockMatyDelete)Worker.Get<HisMediStockMatyDelete>();
            }
        }
        private HisMediStockMatyTruncate TruncateWorker
        {
            get
            {
                return (HisMediStockMatyTruncate)Worker.Get<HisMediStockMatyTruncate>();
            }
        }
        private HisMediStockMatyGet GetWorker
        {
            get
            {
                return (HisMediStockMatyGet)Worker.Get<HisMediStockMatyGet>();
            }
        }
        private HisMediStockMatyCheck CheckWorker
        {
            get
            {
                return (HisMediStockMatyCheck)Worker.Get<HisMediStockMatyCheck>();
            }
        }

        public bool Create(HIS_MEDI_STOCK_MATY data)
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

        public bool CreateList(List<HIS_MEDI_STOCK_MATY> listData)
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

        public bool Update(HIS_MEDI_STOCK_MATY data)
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

        public bool UpdateList(List<HIS_MEDI_STOCK_MATY> listData)
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

        public bool Delete(HIS_MEDI_STOCK_MATY data)
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

        public bool DeleteList(List<HIS_MEDI_STOCK_MATY> listData)
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

        public bool Truncate(HIS_MEDI_STOCK_MATY data)
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

        public bool TruncateList(List<HIS_MEDI_STOCK_MATY> listData)
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

        public List<HIS_MEDI_STOCK_MATY> Get(HisMediStockMatySO search, CommonParam param)
        {
            List<HIS_MEDI_STOCK_MATY> result = new List<HIS_MEDI_STOCK_MATY>();
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

        public HIS_MEDI_STOCK_MATY GetById(long id, HisMediStockMatySO search)
        {
            HIS_MEDI_STOCK_MATY result = null;
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
