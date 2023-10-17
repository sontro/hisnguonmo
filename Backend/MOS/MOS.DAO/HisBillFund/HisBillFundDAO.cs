using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBillFund
{
    public partial class HisBillFundDAO : EntityBase
    {
        private HisBillFundCreate CreateWorker
        {
            get
            {
                return (HisBillFundCreate)Worker.Get<HisBillFundCreate>();
            }
        }
        private HisBillFundUpdate UpdateWorker
        {
            get
            {
                return (HisBillFundUpdate)Worker.Get<HisBillFundUpdate>();
            }
        }
        private HisBillFundDelete DeleteWorker
        {
            get
            {
                return (HisBillFundDelete)Worker.Get<HisBillFundDelete>();
            }
        }
        private HisBillFundTruncate TruncateWorker
        {
            get
            {
                return (HisBillFundTruncate)Worker.Get<HisBillFundTruncate>();
            }
        }
        private HisBillFundGet GetWorker
        {
            get
            {
                return (HisBillFundGet)Worker.Get<HisBillFundGet>();
            }
        }
        private HisBillFundCheck CheckWorker
        {
            get
            {
                return (HisBillFundCheck)Worker.Get<HisBillFundCheck>();
            }
        }

        public bool Create(HIS_BILL_FUND data)
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

        public bool CreateList(List<HIS_BILL_FUND> listData)
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

        public bool Update(HIS_BILL_FUND data)
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

        public bool UpdateList(List<HIS_BILL_FUND> listData)
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

        public bool Delete(HIS_BILL_FUND data)
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

        public bool DeleteList(List<HIS_BILL_FUND> listData)
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

        public bool Truncate(HIS_BILL_FUND data)
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

        public bool TruncateList(List<HIS_BILL_FUND> listData)
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

        public List<HIS_BILL_FUND> Get(HisBillFundSO search, CommonParam param)
        {
            List<HIS_BILL_FUND> result = new List<HIS_BILL_FUND>();
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

        public HIS_BILL_FUND GetById(long id, HisBillFundSO search)
        {
            HIS_BILL_FUND result = null;
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
