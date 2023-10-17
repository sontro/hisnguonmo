using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpUserTempDt
{
    public partial class HisImpUserTempDtDAO : EntityBase
    {
        private HisImpUserTempDtCreate CreateWorker
        {
            get
            {
                return (HisImpUserTempDtCreate)Worker.Get<HisImpUserTempDtCreate>();
            }
        }
        private HisImpUserTempDtUpdate UpdateWorker
        {
            get
            {
                return (HisImpUserTempDtUpdate)Worker.Get<HisImpUserTempDtUpdate>();
            }
        }
        private HisImpUserTempDtDelete DeleteWorker
        {
            get
            {
                return (HisImpUserTempDtDelete)Worker.Get<HisImpUserTempDtDelete>();
            }
        }
        private HisImpUserTempDtTruncate TruncateWorker
        {
            get
            {
                return (HisImpUserTempDtTruncate)Worker.Get<HisImpUserTempDtTruncate>();
            }
        }
        private HisImpUserTempDtGet GetWorker
        {
            get
            {
                return (HisImpUserTempDtGet)Worker.Get<HisImpUserTempDtGet>();
            }
        }
        private HisImpUserTempDtCheck CheckWorker
        {
            get
            {
                return (HisImpUserTempDtCheck)Worker.Get<HisImpUserTempDtCheck>();
            }
        }

        public bool Create(HIS_IMP_USER_TEMP_DT data)
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

        public bool CreateList(List<HIS_IMP_USER_TEMP_DT> listData)
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

        public bool Update(HIS_IMP_USER_TEMP_DT data)
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

        public bool UpdateList(List<HIS_IMP_USER_TEMP_DT> listData)
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

        public bool Delete(HIS_IMP_USER_TEMP_DT data)
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

        public bool DeleteList(List<HIS_IMP_USER_TEMP_DT> listData)
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

        public bool Truncate(HIS_IMP_USER_TEMP_DT data)
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

        public bool TruncateList(List<HIS_IMP_USER_TEMP_DT> listData)
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

        public List<HIS_IMP_USER_TEMP_DT> Get(HisImpUserTempDtSO search, CommonParam param)
        {
            List<HIS_IMP_USER_TEMP_DT> result = new List<HIS_IMP_USER_TEMP_DT>();
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

        public HIS_IMP_USER_TEMP_DT GetById(long id, HisImpUserTempDtSO search)
        {
            HIS_IMP_USER_TEMP_DT result = null;
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
