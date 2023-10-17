using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCashierAddConfig
{
    public partial class HisCashierAddConfigDAO : EntityBase
    {
        private HisCashierAddConfigCreate CreateWorker
        {
            get
            {
                return (HisCashierAddConfigCreate)Worker.Get<HisCashierAddConfigCreate>();
            }
        }
        private HisCashierAddConfigUpdate UpdateWorker
        {
            get
            {
                return (HisCashierAddConfigUpdate)Worker.Get<HisCashierAddConfigUpdate>();
            }
        }
        private HisCashierAddConfigDelete DeleteWorker
        {
            get
            {
                return (HisCashierAddConfigDelete)Worker.Get<HisCashierAddConfigDelete>();
            }
        }
        private HisCashierAddConfigTruncate TruncateWorker
        {
            get
            {
                return (HisCashierAddConfigTruncate)Worker.Get<HisCashierAddConfigTruncate>();
            }
        }
        private HisCashierAddConfigGet GetWorker
        {
            get
            {
                return (HisCashierAddConfigGet)Worker.Get<HisCashierAddConfigGet>();
            }
        }
        private HisCashierAddConfigCheck CheckWorker
        {
            get
            {
                return (HisCashierAddConfigCheck)Worker.Get<HisCashierAddConfigCheck>();
            }
        }

        public bool Create(HIS_CASHIER_ADD_CONFIG data)
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

        public bool CreateList(List<HIS_CASHIER_ADD_CONFIG> listData)
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

        public bool Update(HIS_CASHIER_ADD_CONFIG data)
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

        public bool UpdateList(List<HIS_CASHIER_ADD_CONFIG> listData)
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

        public bool Delete(HIS_CASHIER_ADD_CONFIG data)
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

        public bool DeleteList(List<HIS_CASHIER_ADD_CONFIG> listData)
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

        public bool Truncate(HIS_CASHIER_ADD_CONFIG data)
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

        public bool TruncateList(List<HIS_CASHIER_ADD_CONFIG> listData)
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

        public List<HIS_CASHIER_ADD_CONFIG> Get(HisCashierAddConfigSO search, CommonParam param)
        {
            List<HIS_CASHIER_ADD_CONFIG> result = new List<HIS_CASHIER_ADD_CONFIG>();
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

        public HIS_CASHIER_ADD_CONFIG GetById(long id, HisCashierAddConfigSO search)
        {
            HIS_CASHIER_ADD_CONFIG result = null;
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
