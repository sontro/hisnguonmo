using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExmeReasonCfg
{
    public partial class HisExmeReasonCfgDAO : EntityBase
    {
        private HisExmeReasonCfgCreate CreateWorker
        {
            get
            {
                return (HisExmeReasonCfgCreate)Worker.Get<HisExmeReasonCfgCreate>();
            }
        }
        private HisExmeReasonCfgUpdate UpdateWorker
        {
            get
            {
                return (HisExmeReasonCfgUpdate)Worker.Get<HisExmeReasonCfgUpdate>();
            }
        }
        private HisExmeReasonCfgDelete DeleteWorker
        {
            get
            {
                return (HisExmeReasonCfgDelete)Worker.Get<HisExmeReasonCfgDelete>();
            }
        }
        private HisExmeReasonCfgTruncate TruncateWorker
        {
            get
            {
                return (HisExmeReasonCfgTruncate)Worker.Get<HisExmeReasonCfgTruncate>();
            }
        }
        private HisExmeReasonCfgGet GetWorker
        {
            get
            {
                return (HisExmeReasonCfgGet)Worker.Get<HisExmeReasonCfgGet>();
            }
        }
        private HisExmeReasonCfgCheck CheckWorker
        {
            get
            {
                return (HisExmeReasonCfgCheck)Worker.Get<HisExmeReasonCfgCheck>();
            }
        }

        public bool Create(HIS_EXME_REASON_CFG data)
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

        public bool CreateList(List<HIS_EXME_REASON_CFG> listData)
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

        public bool Update(HIS_EXME_REASON_CFG data)
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

        public bool UpdateList(List<HIS_EXME_REASON_CFG> listData)
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

        public bool Delete(HIS_EXME_REASON_CFG data)
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

        public bool DeleteList(List<HIS_EXME_REASON_CFG> listData)
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

        public bool Truncate(HIS_EXME_REASON_CFG data)
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

        public bool TruncateList(List<HIS_EXME_REASON_CFG> listData)
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

        public List<HIS_EXME_REASON_CFG> Get(HisExmeReasonCfgSO search, CommonParam param)
        {
            List<HIS_EXME_REASON_CFG> result = new List<HIS_EXME_REASON_CFG>();
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

        public HIS_EXME_REASON_CFG GetById(long id, HisExmeReasonCfgSO search)
        {
            HIS_EXME_REASON_CFG result = null;
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
