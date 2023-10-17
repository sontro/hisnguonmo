using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTemp
{
    public partial class HisImpUserTempDAO : EntityBase
    {
        private HisImpUserTempCreate CreateWorker
        {
            get
            {
                return (HisImpUserTempCreate)Worker.Get<HisImpUserTempCreate>();
            }
        }
        private HisImpUserTempUpdate UpdateWorker
        {
            get
            {
                return (HisImpUserTempUpdate)Worker.Get<HisImpUserTempUpdate>();
            }
        }
        private HisImpUserTempDelete DeleteWorker
        {
            get
            {
                return (HisImpUserTempDelete)Worker.Get<HisImpUserTempDelete>();
            }
        }
        private HisImpUserTempTruncate TruncateWorker
        {
            get
            {
                return (HisImpUserTempTruncate)Worker.Get<HisImpUserTempTruncate>();
            }
        }
        private HisImpUserTempGet GetWorker
        {
            get
            {
                return (HisImpUserTempGet)Worker.Get<HisImpUserTempGet>();
            }
        }
        private HisImpUserTempCheck CheckWorker
        {
            get
            {
                return (HisImpUserTempCheck)Worker.Get<HisImpUserTempCheck>();
            }
        }

        public bool Create(HIS_IMP_USER_TEMP data)
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

        public bool CreateList(List<HIS_IMP_USER_TEMP> listData)
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

        public bool Update(HIS_IMP_USER_TEMP data)
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

        public bool UpdateList(List<HIS_IMP_USER_TEMP> listData)
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

        public bool Delete(HIS_IMP_USER_TEMP data)
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

        public bool DeleteList(List<HIS_IMP_USER_TEMP> listData)
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

        public bool Truncate(HIS_IMP_USER_TEMP data)
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

        public bool TruncateList(List<HIS_IMP_USER_TEMP> listData)
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

        public List<HIS_IMP_USER_TEMP> Get(HisImpUserTempSO search, CommonParam param)
        {
            List<HIS_IMP_USER_TEMP> result = new List<HIS_IMP_USER_TEMP>();
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

        public HIS_IMP_USER_TEMP GetById(long id, HisImpUserTempSO search)
        {
            HIS_IMP_USER_TEMP result = null;
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
