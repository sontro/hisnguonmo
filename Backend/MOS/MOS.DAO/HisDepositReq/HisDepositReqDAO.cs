using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReq
{
    public partial class HisDepositReqDAO : EntityBase
    {
        private HisDepositReqCreate CreateWorker
        {
            get
            {
                return (HisDepositReqCreate)Worker.Get<HisDepositReqCreate>();
            }
        }
        private HisDepositReqUpdate UpdateWorker
        {
            get
            {
                return (HisDepositReqUpdate)Worker.Get<HisDepositReqUpdate>();
            }
        }
        private HisDepositReqDelete DeleteWorker
        {
            get
            {
                return (HisDepositReqDelete)Worker.Get<HisDepositReqDelete>();
            }
        }
        private HisDepositReqTruncate TruncateWorker
        {
            get
            {
                return (HisDepositReqTruncate)Worker.Get<HisDepositReqTruncate>();
            }
        }
        private HisDepositReqGet GetWorker
        {
            get
            {
                return (HisDepositReqGet)Worker.Get<HisDepositReqGet>();
            }
        }
        private HisDepositReqCheck CheckWorker
        {
            get
            {
                return (HisDepositReqCheck)Worker.Get<HisDepositReqCheck>();
            }
        }

        public bool Create(HIS_DEPOSIT_REQ data)
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

        public bool CreateList(List<HIS_DEPOSIT_REQ> listData)
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

        public bool Update(HIS_DEPOSIT_REQ data)
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

        public bool UpdateList(List<HIS_DEPOSIT_REQ> listData)
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

        public bool Delete(HIS_DEPOSIT_REQ data)
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

        public bool DeleteList(List<HIS_DEPOSIT_REQ> listData)
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

        public bool Truncate(HIS_DEPOSIT_REQ data)
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

        public bool TruncateList(List<HIS_DEPOSIT_REQ> listData)
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

        public List<HIS_DEPOSIT_REQ> Get(HisDepositReqSO search, CommonParam param)
        {
            List<HIS_DEPOSIT_REQ> result = new List<HIS_DEPOSIT_REQ>();
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

        public HIS_DEPOSIT_REQ GetById(long id, HisDepositReqSO search)
        {
            HIS_DEPOSIT_REQ result = null;
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
