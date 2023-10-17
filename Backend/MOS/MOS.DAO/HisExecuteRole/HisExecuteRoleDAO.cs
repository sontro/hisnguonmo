using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExecuteRole
{
    public partial class HisExecuteRoleDAO : EntityBase
    {
        private HisExecuteRoleCreate CreateWorker
        {
            get
            {
                return (HisExecuteRoleCreate)Worker.Get<HisExecuteRoleCreate>();
            }
        }
        private HisExecuteRoleUpdate UpdateWorker
        {
            get
            {
                return (HisExecuteRoleUpdate)Worker.Get<HisExecuteRoleUpdate>();
            }
        }
        private HisExecuteRoleDelete DeleteWorker
        {
            get
            {
                return (HisExecuteRoleDelete)Worker.Get<HisExecuteRoleDelete>();
            }
        }
        private HisExecuteRoleTruncate TruncateWorker
        {
            get
            {
                return (HisExecuteRoleTruncate)Worker.Get<HisExecuteRoleTruncate>();
            }
        }
        private HisExecuteRoleGet GetWorker
        {
            get
            {
                return (HisExecuteRoleGet)Worker.Get<HisExecuteRoleGet>();
            }
        }
        private HisExecuteRoleCheck CheckWorker
        {
            get
            {
                return (HisExecuteRoleCheck)Worker.Get<HisExecuteRoleCheck>();
            }
        }

        public bool Create(HIS_EXECUTE_ROLE data)
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

        public bool CreateList(List<HIS_EXECUTE_ROLE> listData)
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

        public bool Update(HIS_EXECUTE_ROLE data)
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

        public bool UpdateList(List<HIS_EXECUTE_ROLE> listData)
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

        public bool Delete(HIS_EXECUTE_ROLE data)
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

        public bool DeleteList(List<HIS_EXECUTE_ROLE> listData)
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

        public bool Truncate(HIS_EXECUTE_ROLE data)
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

        public bool TruncateList(List<HIS_EXECUTE_ROLE> listData)
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

        public List<HIS_EXECUTE_ROLE> Get(HisExecuteRoleSO search, CommonParam param)
        {
            List<HIS_EXECUTE_ROLE> result = new List<HIS_EXECUTE_ROLE>();
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

        public HIS_EXECUTE_ROLE GetById(long id, HisExecuteRoleSO search)
        {
            HIS_EXECUTE_ROLE result = null;
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
