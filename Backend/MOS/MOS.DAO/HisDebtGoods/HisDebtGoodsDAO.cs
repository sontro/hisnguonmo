using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDebtGoods
{
    public partial class HisDebtGoodsDAO : EntityBase
    {
        private HisDebtGoodsCreate CreateWorker
        {
            get
            {
                return (HisDebtGoodsCreate)Worker.Get<HisDebtGoodsCreate>();
            }
        }
        private HisDebtGoodsUpdate UpdateWorker
        {
            get
            {
                return (HisDebtGoodsUpdate)Worker.Get<HisDebtGoodsUpdate>();
            }
        }
        private HisDebtGoodsDelete DeleteWorker
        {
            get
            {
                return (HisDebtGoodsDelete)Worker.Get<HisDebtGoodsDelete>();
            }
        }
        private HisDebtGoodsTruncate TruncateWorker
        {
            get
            {
                return (HisDebtGoodsTruncate)Worker.Get<HisDebtGoodsTruncate>();
            }
        }
        private HisDebtGoodsGet GetWorker
        {
            get
            {
                return (HisDebtGoodsGet)Worker.Get<HisDebtGoodsGet>();
            }
        }
        private HisDebtGoodsCheck CheckWorker
        {
            get
            {
                return (HisDebtGoodsCheck)Worker.Get<HisDebtGoodsCheck>();
            }
        }

        public bool Create(HIS_DEBT_GOODS data)
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

        public bool CreateList(List<HIS_DEBT_GOODS> listData)
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

        public bool Update(HIS_DEBT_GOODS data)
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

        public bool UpdateList(List<HIS_DEBT_GOODS> listData)
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

        public bool Delete(HIS_DEBT_GOODS data)
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

        public bool DeleteList(List<HIS_DEBT_GOODS> listData)
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

        public bool Truncate(HIS_DEBT_GOODS data)
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

        public bool TruncateList(List<HIS_DEBT_GOODS> listData)
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

        public List<HIS_DEBT_GOODS> Get(HisDebtGoodsSO search, CommonParam param)
        {
            List<HIS_DEBT_GOODS> result = new List<HIS_DEBT_GOODS>();
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

        public HIS_DEBT_GOODS GetById(long id, HisDebtGoodsSO search)
        {
            HIS_DEBT_GOODS result = null;
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
