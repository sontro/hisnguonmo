using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExpiredDateCfg
{
    public partial class HisExpiredDateCfgDAO : EntityBase
    {
        private HisExpiredDateCfgCreate CreateWorker
        {
            get
            {
                return (HisExpiredDateCfgCreate)Worker.Get<HisExpiredDateCfgCreate>();
            }
        }
        private HisExpiredDateCfgUpdate UpdateWorker
        {
            get
            {
                return (HisExpiredDateCfgUpdate)Worker.Get<HisExpiredDateCfgUpdate>();
            }
        }
        private HisExpiredDateCfgDelete DeleteWorker
        {
            get
            {
                return (HisExpiredDateCfgDelete)Worker.Get<HisExpiredDateCfgDelete>();
            }
        }
        private HisExpiredDateCfgTruncate TruncateWorker
        {
            get
            {
                return (HisExpiredDateCfgTruncate)Worker.Get<HisExpiredDateCfgTruncate>();
            }
        }
        private HisExpiredDateCfgGet GetWorker
        {
            get
            {
                return (HisExpiredDateCfgGet)Worker.Get<HisExpiredDateCfgGet>();
            }
        }
        private HisExpiredDateCfgCheck CheckWorker
        {
            get
            {
                return (HisExpiredDateCfgCheck)Worker.Get<HisExpiredDateCfgCheck>();
            }
        }

        public bool Create(HIS_EXPIRED_DATE_CFG data)
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

        public bool CreateList(List<HIS_EXPIRED_DATE_CFG> listData)
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

        public bool Update(HIS_EXPIRED_DATE_CFG data)
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

        public bool UpdateList(List<HIS_EXPIRED_DATE_CFG> listData)
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

        public bool Delete(HIS_EXPIRED_DATE_CFG data)
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

        public bool DeleteList(List<HIS_EXPIRED_DATE_CFG> listData)
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

        public bool Truncate(HIS_EXPIRED_DATE_CFG data)
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

        public bool TruncateList(List<HIS_EXPIRED_DATE_CFG> listData)
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

        public List<HIS_EXPIRED_DATE_CFG> Get(HisExpiredDateCfgSO search, CommonParam param)
        {
            List<HIS_EXPIRED_DATE_CFG> result = new List<HIS_EXPIRED_DATE_CFG>();
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

        public HIS_EXPIRED_DATE_CFG GetById(long id, HisExpiredDateCfgSO search)
        {
            HIS_EXPIRED_DATE_CFG result = null;
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
