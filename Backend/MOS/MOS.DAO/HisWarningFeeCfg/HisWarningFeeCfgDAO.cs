using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisWarningFeeCfg
{
    public partial class HisWarningFeeCfgDAO : EntityBase
    {
        private HisWarningFeeCfgCreate CreateWorker
        {
            get
            {
                return (HisWarningFeeCfgCreate)Worker.Get<HisWarningFeeCfgCreate>();
            }
        }
        private HisWarningFeeCfgUpdate UpdateWorker
        {
            get
            {
                return (HisWarningFeeCfgUpdate)Worker.Get<HisWarningFeeCfgUpdate>();
            }
        }
        private HisWarningFeeCfgDelete DeleteWorker
        {
            get
            {
                return (HisWarningFeeCfgDelete)Worker.Get<HisWarningFeeCfgDelete>();
            }
        }
        private HisWarningFeeCfgTruncate TruncateWorker
        {
            get
            {
                return (HisWarningFeeCfgTruncate)Worker.Get<HisWarningFeeCfgTruncate>();
            }
        }
        private HisWarningFeeCfgGet GetWorker
        {
            get
            {
                return (HisWarningFeeCfgGet)Worker.Get<HisWarningFeeCfgGet>();
            }
        }
        private HisWarningFeeCfgCheck CheckWorker
        {
            get
            {
                return (HisWarningFeeCfgCheck)Worker.Get<HisWarningFeeCfgCheck>();
            }
        }

        public bool Create(HIS_WARNING_FEE_CFG data)
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

        public bool CreateList(List<HIS_WARNING_FEE_CFG> listData)
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

        public bool Update(HIS_WARNING_FEE_CFG data)
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

        public bool UpdateList(List<HIS_WARNING_FEE_CFG> listData)
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

        public bool Delete(HIS_WARNING_FEE_CFG data)
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

        public bool DeleteList(List<HIS_WARNING_FEE_CFG> listData)
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

        public bool Truncate(HIS_WARNING_FEE_CFG data)
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

        public bool TruncateList(List<HIS_WARNING_FEE_CFG> listData)
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

        public List<HIS_WARNING_FEE_CFG> Get(HisWarningFeeCfgSO search, CommonParam param)
        {
            List<HIS_WARNING_FEE_CFG> result = new List<HIS_WARNING_FEE_CFG>();
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

        public HIS_WARNING_FEE_CFG GetById(long id, HisWarningFeeCfgSO search)
        {
            HIS_WARNING_FEE_CFG result = null;
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
