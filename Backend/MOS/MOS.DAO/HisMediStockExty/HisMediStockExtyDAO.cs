using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMediStockExty
{
    public partial class HisMediStockExtyDAO : EntityBase
    {
        private HisMediStockExtyCreate CreateWorker
        {
            get
            {
                return (HisMediStockExtyCreate)Worker.Get<HisMediStockExtyCreate>();
            }
        }
        private HisMediStockExtyUpdate UpdateWorker
        {
            get
            {
                return (HisMediStockExtyUpdate)Worker.Get<HisMediStockExtyUpdate>();
            }
        }
        private HisMediStockExtyDelete DeleteWorker
        {
            get
            {
                return (HisMediStockExtyDelete)Worker.Get<HisMediStockExtyDelete>();
            }
        }
        private HisMediStockExtyTruncate TruncateWorker
        {
            get
            {
                return (HisMediStockExtyTruncate)Worker.Get<HisMediStockExtyTruncate>();
            }
        }
        private HisMediStockExtyGet GetWorker
        {
            get
            {
                return (HisMediStockExtyGet)Worker.Get<HisMediStockExtyGet>();
            }
        }
        private HisMediStockExtyCheck CheckWorker
        {
            get
            {
                return (HisMediStockExtyCheck)Worker.Get<HisMediStockExtyCheck>();
            }
        }

        public bool Create(HIS_MEDI_STOCK_EXTY data)
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

        public bool CreateList(List<HIS_MEDI_STOCK_EXTY> listData)
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

        public bool Update(HIS_MEDI_STOCK_EXTY data)
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

        public bool UpdateList(List<HIS_MEDI_STOCK_EXTY> listData)
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

        public bool Delete(HIS_MEDI_STOCK_EXTY data)
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

        public bool DeleteList(List<HIS_MEDI_STOCK_EXTY> listData)
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

        public bool Truncate(HIS_MEDI_STOCK_EXTY data)
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

        public bool TruncateList(List<HIS_MEDI_STOCK_EXTY> listData)
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

        public List<HIS_MEDI_STOCK_EXTY> Get(HisMediStockExtySO search, CommonParam param)
        {
            List<HIS_MEDI_STOCK_EXTY> result = new List<HIS_MEDI_STOCK_EXTY>();
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

        public HIS_MEDI_STOCK_EXTY GetById(long id, HisMediStockExtySO search)
        {
            HIS_MEDI_STOCK_EXTY result = null;
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
