using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestReason
{
    public partial class HisExpMestReasonDAO : EntityBase
    {
        private HisExpMestReasonCreate CreateWorker
        {
            get
            {
                return (HisExpMestReasonCreate)Worker.Get<HisExpMestReasonCreate>();
            }
        }
        private HisExpMestReasonUpdate UpdateWorker
        {
            get
            {
                return (HisExpMestReasonUpdate)Worker.Get<HisExpMestReasonUpdate>();
            }
        }
        private HisExpMestReasonDelete DeleteWorker
        {
            get
            {
                return (HisExpMestReasonDelete)Worker.Get<HisExpMestReasonDelete>();
            }
        }
        private HisExpMestReasonTruncate TruncateWorker
        {
            get
            {
                return (HisExpMestReasonTruncate)Worker.Get<HisExpMestReasonTruncate>();
            }
        }
        private HisExpMestReasonGet GetWorker
        {
            get
            {
                return (HisExpMestReasonGet)Worker.Get<HisExpMestReasonGet>();
            }
        }
        private HisExpMestReasonCheck CheckWorker
        {
            get
            {
                return (HisExpMestReasonCheck)Worker.Get<HisExpMestReasonCheck>();
            }
        }

        public bool Create(HIS_EXP_MEST_REASON data)
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

        public bool CreateList(List<HIS_EXP_MEST_REASON> listData)
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

        public bool Update(HIS_EXP_MEST_REASON data)
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

        public bool UpdateList(List<HIS_EXP_MEST_REASON> listData)
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

        public bool Delete(HIS_EXP_MEST_REASON data)
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

        public bool DeleteList(List<HIS_EXP_MEST_REASON> listData)
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

        public bool Truncate(HIS_EXP_MEST_REASON data)
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

        public bool TruncateList(List<HIS_EXP_MEST_REASON> listData)
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

        public List<HIS_EXP_MEST_REASON> Get(HisExpMestReasonSO search, CommonParam param)
        {
            List<HIS_EXP_MEST_REASON> result = new List<HIS_EXP_MEST_REASON>();
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

        public HIS_EXP_MEST_REASON GetById(long id, HisExpMestReasonSO search)
        {
            HIS_EXP_MEST_REASON result = null;
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
