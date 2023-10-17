using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRejectAlert
{
    public partial class HisRejectAlertDAO : EntityBase
    {
        private HisRejectAlertCreate CreateWorker
        {
            get
            {
                return (HisRejectAlertCreate)Worker.Get<HisRejectAlertCreate>();
            }
        }
        private HisRejectAlertUpdate UpdateWorker
        {
            get
            {
                return (HisRejectAlertUpdate)Worker.Get<HisRejectAlertUpdate>();
            }
        }
        private HisRejectAlertDelete DeleteWorker
        {
            get
            {
                return (HisRejectAlertDelete)Worker.Get<HisRejectAlertDelete>();
            }
        }
        private HisRejectAlertTruncate TruncateWorker
        {
            get
            {
                return (HisRejectAlertTruncate)Worker.Get<HisRejectAlertTruncate>();
            }
        }
        private HisRejectAlertGet GetWorker
        {
            get
            {
                return (HisRejectAlertGet)Worker.Get<HisRejectAlertGet>();
            }
        }
        private HisRejectAlertCheck CheckWorker
        {
            get
            {
                return (HisRejectAlertCheck)Worker.Get<HisRejectAlertCheck>();
            }
        }

        public bool Create(HIS_REJECT_ALERT data)
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

        public bool CreateList(List<HIS_REJECT_ALERT> listData)
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

        public bool Update(HIS_REJECT_ALERT data)
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

        public bool UpdateList(List<HIS_REJECT_ALERT> listData)
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

        public bool Delete(HIS_REJECT_ALERT data)
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

        public bool DeleteList(List<HIS_REJECT_ALERT> listData)
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

        public bool Truncate(HIS_REJECT_ALERT data)
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

        public bool TruncateList(List<HIS_REJECT_ALERT> listData)
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

        public List<HIS_REJECT_ALERT> Get(HisRejectAlertSO search, CommonParam param)
        {
            List<HIS_REJECT_ALERT> result = new List<HIS_REJECT_ALERT>();
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

        public HIS_REJECT_ALERT GetById(long id, HisRejectAlertSO search)
        {
            HIS_REJECT_ALERT result = null;
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
