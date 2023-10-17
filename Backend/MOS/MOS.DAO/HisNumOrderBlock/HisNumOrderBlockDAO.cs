using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderBlock
{
    public partial class HisNumOrderBlockDAO : EntityBase
    {
        private HisNumOrderBlockCreate CreateWorker
        {
            get
            {
                return (HisNumOrderBlockCreate)Worker.Get<HisNumOrderBlockCreate>();
            }
        }
        private HisNumOrderBlockUpdate UpdateWorker
        {
            get
            {
                return (HisNumOrderBlockUpdate)Worker.Get<HisNumOrderBlockUpdate>();
            }
        }
        private HisNumOrderBlockDelete DeleteWorker
        {
            get
            {
                return (HisNumOrderBlockDelete)Worker.Get<HisNumOrderBlockDelete>();
            }
        }
        private HisNumOrderBlockTruncate TruncateWorker
        {
            get
            {
                return (HisNumOrderBlockTruncate)Worker.Get<HisNumOrderBlockTruncate>();
            }
        }
        private HisNumOrderBlockGet GetWorker
        {
            get
            {
                return (HisNumOrderBlockGet)Worker.Get<HisNumOrderBlockGet>();
            }
        }
        private HisNumOrderBlockCheck CheckWorker
        {
            get
            {
                return (HisNumOrderBlockCheck)Worker.Get<HisNumOrderBlockCheck>();
            }
        }

        public bool Create(HIS_NUM_ORDER_BLOCK data)
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

        public bool CreateList(List<HIS_NUM_ORDER_BLOCK> listData)
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

        public bool Update(HIS_NUM_ORDER_BLOCK data)
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

        public bool UpdateList(List<HIS_NUM_ORDER_BLOCK> listData)
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

        public bool Delete(HIS_NUM_ORDER_BLOCK data)
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

        public bool DeleteList(List<HIS_NUM_ORDER_BLOCK> listData)
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

        public bool Truncate(HIS_NUM_ORDER_BLOCK data)
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

        public bool TruncateList(List<HIS_NUM_ORDER_BLOCK> listData)
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

        public List<HIS_NUM_ORDER_BLOCK> Get(HisNumOrderBlockSO search, CommonParam param)
        {
            List<HIS_NUM_ORDER_BLOCK> result = new List<HIS_NUM_ORDER_BLOCK>();
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

        public HIS_NUM_ORDER_BLOCK GetById(long id, HisNumOrderBlockSO search)
        {
            HIS_NUM_ORDER_BLOCK result = null;
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
