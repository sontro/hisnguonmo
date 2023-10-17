using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTransactionExp
{
    public partial class HisTransactionExpDAO : EntityBase
    {
        private HisTransactionExpCreate CreateWorker
        {
            get
            {
                return (HisTransactionExpCreate)Worker.Get<HisTransactionExpCreate>();
            }
        }
        private HisTransactionExpUpdate UpdateWorker
        {
            get
            {
                return (HisTransactionExpUpdate)Worker.Get<HisTransactionExpUpdate>();
            }
        }
        private HisTransactionExpDelete DeleteWorker
        {
            get
            {
                return (HisTransactionExpDelete)Worker.Get<HisTransactionExpDelete>();
            }
        }
        private HisTransactionExpTruncate TruncateWorker
        {
            get
            {
                return (HisTransactionExpTruncate)Worker.Get<HisTransactionExpTruncate>();
            }
        }
        private HisTransactionExpGet GetWorker
        {
            get
            {
                return (HisTransactionExpGet)Worker.Get<HisTransactionExpGet>();
            }
        }
        private HisTransactionExpCheck CheckWorker
        {
            get
            {
                return (HisTransactionExpCheck)Worker.Get<HisTransactionExpCheck>();
            }
        }

        public bool Create(HIS_TRANSACTION_EXP data)
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

        public bool CreateList(List<HIS_TRANSACTION_EXP> listData)
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

        public bool Update(HIS_TRANSACTION_EXP data)
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

        public bool UpdateList(List<HIS_TRANSACTION_EXP> listData)
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

        public bool Delete(HIS_TRANSACTION_EXP data)
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

        public bool DeleteList(List<HIS_TRANSACTION_EXP> listData)
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

        public bool Truncate(HIS_TRANSACTION_EXP data)
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

        public bool TruncateList(List<HIS_TRANSACTION_EXP> listData)
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

        public List<HIS_TRANSACTION_EXP> Get(HisTransactionExpSO search, CommonParam param)
        {
            List<HIS_TRANSACTION_EXP> result = new List<HIS_TRANSACTION_EXP>();
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

        public HIS_TRANSACTION_EXP GetById(long id, HisTransactionExpSO search)
        {
            HIS_TRANSACTION_EXP result = null;
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
