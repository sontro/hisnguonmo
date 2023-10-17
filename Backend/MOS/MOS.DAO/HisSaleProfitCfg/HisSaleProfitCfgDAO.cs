using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSaleProfitCfg
{
    public partial class HisSaleProfitCfgDAO : EntityBase
    {
        private HisSaleProfitCfgCreate CreateWorker
        {
            get
            {
                return (HisSaleProfitCfgCreate)Worker.Get<HisSaleProfitCfgCreate>();
            }
        }
        private HisSaleProfitCfgUpdate UpdateWorker
        {
            get
            {
                return (HisSaleProfitCfgUpdate)Worker.Get<HisSaleProfitCfgUpdate>();
            }
        }
        private HisSaleProfitCfgDelete DeleteWorker
        {
            get
            {
                return (HisSaleProfitCfgDelete)Worker.Get<HisSaleProfitCfgDelete>();
            }
        }
        private HisSaleProfitCfgTruncate TruncateWorker
        {
            get
            {
                return (HisSaleProfitCfgTruncate)Worker.Get<HisSaleProfitCfgTruncate>();
            }
        }
        private HisSaleProfitCfgGet GetWorker
        {
            get
            {
                return (HisSaleProfitCfgGet)Worker.Get<HisSaleProfitCfgGet>();
            }
        }
        private HisSaleProfitCfgCheck CheckWorker
        {
            get
            {
                return (HisSaleProfitCfgCheck)Worker.Get<HisSaleProfitCfgCheck>();
            }
        }

        public bool Create(HIS_SALE_PROFIT_CFG data)
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

        public bool CreateList(List<HIS_SALE_PROFIT_CFG> listData)
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

        public bool Update(HIS_SALE_PROFIT_CFG data)
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

        public bool UpdateList(List<HIS_SALE_PROFIT_CFG> listData)
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

        public bool Delete(HIS_SALE_PROFIT_CFG data)
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

        public bool DeleteList(List<HIS_SALE_PROFIT_CFG> listData)
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

        public bool Truncate(HIS_SALE_PROFIT_CFG data)
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

        public bool TruncateList(List<HIS_SALE_PROFIT_CFG> listData)
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

        public List<HIS_SALE_PROFIT_CFG> Get(HisSaleProfitCfgSO search, CommonParam param)
        {
            List<HIS_SALE_PROFIT_CFG> result = new List<HIS_SALE_PROFIT_CFG>();
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

        public HIS_SALE_PROFIT_CFG GetById(long id, HisSaleProfitCfgSO search)
        {
            HIS_SALE_PROFIT_CFG result = null;
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
