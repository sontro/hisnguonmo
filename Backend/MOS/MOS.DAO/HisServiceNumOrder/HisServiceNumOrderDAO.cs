using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceNumOrder
{
    public partial class HisServiceNumOrderDAO : EntityBase
    {
        private HisServiceNumOrderCreate CreateWorker
        {
            get
            {
                return (HisServiceNumOrderCreate)Worker.Get<HisServiceNumOrderCreate>();
            }
        }
        private HisServiceNumOrderUpdate UpdateWorker
        {
            get
            {
                return (HisServiceNumOrderUpdate)Worker.Get<HisServiceNumOrderUpdate>();
            }
        }
        private HisServiceNumOrderDelete DeleteWorker
        {
            get
            {
                return (HisServiceNumOrderDelete)Worker.Get<HisServiceNumOrderDelete>();
            }
        }
        private HisServiceNumOrderTruncate TruncateWorker
        {
            get
            {
                return (HisServiceNumOrderTruncate)Worker.Get<HisServiceNumOrderTruncate>();
            }
        }
        private HisServiceNumOrderGet GetWorker
        {
            get
            {
                return (HisServiceNumOrderGet)Worker.Get<HisServiceNumOrderGet>();
            }
        }
        private HisServiceNumOrderCheck CheckWorker
        {
            get
            {
                return (HisServiceNumOrderCheck)Worker.Get<HisServiceNumOrderCheck>();
            }
        }

        public bool Create(HIS_SERVICE_NUM_ORDER data)
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

        public bool CreateList(List<HIS_SERVICE_NUM_ORDER> listData)
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

        public bool Update(HIS_SERVICE_NUM_ORDER data)
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

        public bool UpdateList(List<HIS_SERVICE_NUM_ORDER> listData)
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

        public bool Delete(HIS_SERVICE_NUM_ORDER data)
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

        public bool DeleteList(List<HIS_SERVICE_NUM_ORDER> listData)
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

        public bool Truncate(HIS_SERVICE_NUM_ORDER data)
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

        public bool TruncateList(List<HIS_SERVICE_NUM_ORDER> listData)
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

        public List<HIS_SERVICE_NUM_ORDER> Get(HisServiceNumOrderSO search, CommonParam param)
        {
            List<HIS_SERVICE_NUM_ORDER> result = new List<HIS_SERVICE_NUM_ORDER>();
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

        public HIS_SERVICE_NUM_ORDER GetById(long id, HisServiceNumOrderSO search)
        {
            HIS_SERVICE_NUM_ORDER result = null;
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
