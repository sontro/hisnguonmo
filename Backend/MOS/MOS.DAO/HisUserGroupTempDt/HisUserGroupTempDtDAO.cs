using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisUserGroupTempDt
{
    public partial class HisUserGroupTempDtDAO : EntityBase
    {
        private HisUserGroupTempDtCreate CreateWorker
        {
            get
            {
                return (HisUserGroupTempDtCreate)Worker.Get<HisUserGroupTempDtCreate>();
            }
        }
        private HisUserGroupTempDtUpdate UpdateWorker
        {
            get
            {
                return (HisUserGroupTempDtUpdate)Worker.Get<HisUserGroupTempDtUpdate>();
            }
        }
        private HisUserGroupTempDtDelete DeleteWorker
        {
            get
            {
                return (HisUserGroupTempDtDelete)Worker.Get<HisUserGroupTempDtDelete>();
            }
        }
        private HisUserGroupTempDtTruncate TruncateWorker
        {
            get
            {
                return (HisUserGroupTempDtTruncate)Worker.Get<HisUserGroupTempDtTruncate>();
            }
        }
        private HisUserGroupTempDtGet GetWorker
        {
            get
            {
                return (HisUserGroupTempDtGet)Worker.Get<HisUserGroupTempDtGet>();
            }
        }
        private HisUserGroupTempDtCheck CheckWorker
        {
            get
            {
                return (HisUserGroupTempDtCheck)Worker.Get<HisUserGroupTempDtCheck>();
            }
        }

        public bool Create(HIS_USER_GROUP_TEMP_DT data)
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

        public bool CreateList(List<HIS_USER_GROUP_TEMP_DT> listData)
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

        public bool Update(HIS_USER_GROUP_TEMP_DT data)
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

        public bool UpdateList(List<HIS_USER_GROUP_TEMP_DT> listData)
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

        public bool Delete(HIS_USER_GROUP_TEMP_DT data)
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

        public bool DeleteList(List<HIS_USER_GROUP_TEMP_DT> listData)
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

        public bool Truncate(HIS_USER_GROUP_TEMP_DT data)
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

        public bool TruncateList(List<HIS_USER_GROUP_TEMP_DT> listData)
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

        public List<HIS_USER_GROUP_TEMP_DT> Get(HisUserGroupTempDtSO search, CommonParam param)
        {
            List<HIS_USER_GROUP_TEMP_DT> result = new List<HIS_USER_GROUP_TEMP_DT>();
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

        public HIS_USER_GROUP_TEMP_DT GetById(long id, HisUserGroupTempDtSO search)
        {
            HIS_USER_GROUP_TEMP_DT result = null;
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
