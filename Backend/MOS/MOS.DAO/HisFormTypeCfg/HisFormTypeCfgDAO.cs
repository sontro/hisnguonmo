using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisFormTypeCfg
{
    public partial class HisFormTypeCfgDAO : EntityBase
    {
        private HisFormTypeCfgCreate CreateWorker
        {
            get
            {
                return (HisFormTypeCfgCreate)Worker.Get<HisFormTypeCfgCreate>();
            }
        }
        private HisFormTypeCfgUpdate UpdateWorker
        {
            get
            {
                return (HisFormTypeCfgUpdate)Worker.Get<HisFormTypeCfgUpdate>();
            }
        }
        private HisFormTypeCfgDelete DeleteWorker
        {
            get
            {
                return (HisFormTypeCfgDelete)Worker.Get<HisFormTypeCfgDelete>();
            }
        }
        private HisFormTypeCfgTruncate TruncateWorker
        {
            get
            {
                return (HisFormTypeCfgTruncate)Worker.Get<HisFormTypeCfgTruncate>();
            }
        }
        private HisFormTypeCfgGet GetWorker
        {
            get
            {
                return (HisFormTypeCfgGet)Worker.Get<HisFormTypeCfgGet>();
            }
        }
        private HisFormTypeCfgCheck CheckWorker
        {
            get
            {
                return (HisFormTypeCfgCheck)Worker.Get<HisFormTypeCfgCheck>();
            }
        }

        public bool Create(HIS_FORM_TYPE_CFG data)
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

        public bool CreateList(List<HIS_FORM_TYPE_CFG> listData)
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

        public bool Update(HIS_FORM_TYPE_CFG data)
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

        public bool UpdateList(List<HIS_FORM_TYPE_CFG> listData)
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

        public bool Delete(HIS_FORM_TYPE_CFG data)
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

        public bool DeleteList(List<HIS_FORM_TYPE_CFG> listData)
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

        public bool Truncate(HIS_FORM_TYPE_CFG data)
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

        public bool TruncateList(List<HIS_FORM_TYPE_CFG> listData)
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

        public List<HIS_FORM_TYPE_CFG> Get(HisFormTypeCfgSO search, CommonParam param)
        {
            List<HIS_FORM_TYPE_CFG> result = new List<HIS_FORM_TYPE_CFG>();
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

        public HIS_FORM_TYPE_CFG GetById(long id, HisFormTypeCfgSO search)
        {
            HIS_FORM_TYPE_CFG result = null;
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
