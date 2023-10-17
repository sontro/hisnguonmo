using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRoleUser
{
    public partial class HisExecuteRoleUserDAO : EntityBase
    {
        private HisExecuteRoleUserCreate CreateWorker
        {
            get
            {
                return (HisExecuteRoleUserCreate)Worker.Get<HisExecuteRoleUserCreate>();
            }
        }
        private HisExecuteRoleUserUpdate UpdateWorker
        {
            get
            {
                return (HisExecuteRoleUserUpdate)Worker.Get<HisExecuteRoleUserUpdate>();
            }
        }
        private HisExecuteRoleUserDelete DeleteWorker
        {
            get
            {
                return (HisExecuteRoleUserDelete)Worker.Get<HisExecuteRoleUserDelete>();
            }
        }
        private HisExecuteRoleUserTruncate TruncateWorker
        {
            get
            {
                return (HisExecuteRoleUserTruncate)Worker.Get<HisExecuteRoleUserTruncate>();
            }
        }
        private HisExecuteRoleUserGet GetWorker
        {
            get
            {
                return (HisExecuteRoleUserGet)Worker.Get<HisExecuteRoleUserGet>();
            }
        }
        private HisExecuteRoleUserCheck CheckWorker
        {
            get
            {
                return (HisExecuteRoleUserCheck)Worker.Get<HisExecuteRoleUserCheck>();
            }
        }

        public bool Create(HIS_EXECUTE_ROLE_USER data)
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

        public bool CreateList(List<HIS_EXECUTE_ROLE_USER> listData)
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

        public bool Update(HIS_EXECUTE_ROLE_USER data)
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

        public bool UpdateList(List<HIS_EXECUTE_ROLE_USER> listData)
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

        public bool Delete(HIS_EXECUTE_ROLE_USER data)
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

        public bool DeleteList(List<HIS_EXECUTE_ROLE_USER> listData)
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

        public bool Truncate(HIS_EXECUTE_ROLE_USER data)
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

        public bool TruncateList(List<HIS_EXECUTE_ROLE_USER> listData)
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

        public List<HIS_EXECUTE_ROLE_USER> Get(HisExecuteRoleUserSO search, CommonParam param)
        {
            List<HIS_EXECUTE_ROLE_USER> result = new List<HIS_EXECUTE_ROLE_USER>();
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

        public HIS_EXECUTE_ROLE_USER GetById(long id, HisExecuteRoleUserSO search)
        {
            HIS_EXECUTE_ROLE_USER result = null;
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
