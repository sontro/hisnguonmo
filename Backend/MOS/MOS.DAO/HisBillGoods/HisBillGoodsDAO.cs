using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBillGoods
{
    public partial class HisBillGoodsDAO : EntityBase
    {
        private HisBillGoodsCreate CreateWorker
        {
            get
            {
                return (HisBillGoodsCreate)Worker.Get<HisBillGoodsCreate>();
            }
        }
        private HisBillGoodsUpdate UpdateWorker
        {
            get
            {
                return (HisBillGoodsUpdate)Worker.Get<HisBillGoodsUpdate>();
            }
        }
        private HisBillGoodsDelete DeleteWorker
        {
            get
            {
                return (HisBillGoodsDelete)Worker.Get<HisBillGoodsDelete>();
            }
        }
        private HisBillGoodsTruncate TruncateWorker
        {
            get
            {
                return (HisBillGoodsTruncate)Worker.Get<HisBillGoodsTruncate>();
            }
        }
        private HisBillGoodsGet GetWorker
        {
            get
            {
                return (HisBillGoodsGet)Worker.Get<HisBillGoodsGet>();
            }
        }
        private HisBillGoodsCheck CheckWorker
        {
            get
            {
                return (HisBillGoodsCheck)Worker.Get<HisBillGoodsCheck>();
            }
        }

        public bool Create(HIS_BILL_GOODS data)
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

        public bool CreateList(List<HIS_BILL_GOODS> listData)
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

        public bool Update(HIS_BILL_GOODS data)
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

        public bool UpdateList(List<HIS_BILL_GOODS> listData)
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

        public bool Delete(HIS_BILL_GOODS data)
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

        public bool DeleteList(List<HIS_BILL_GOODS> listData)
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

        public bool Truncate(HIS_BILL_GOODS data)
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

        public bool TruncateList(List<HIS_BILL_GOODS> listData)
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

        public List<HIS_BILL_GOODS> Get(HisBillGoodsSO search, CommonParam param)
        {
            List<HIS_BILL_GOODS> result = new List<HIS_BILL_GOODS>();
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

        public HIS_BILL_GOODS GetById(long id, HisBillGoodsSO search)
        {
            HIS_BILL_GOODS result = null;
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
