using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsModuleRole
{
    public partial class AcsModuleRoleDAO : EntityBase
    {
        private AcsModuleRoleCreate CreateWorker
        {
            get
            {
                return (AcsModuleRoleCreate)Worker.Get<AcsModuleRoleCreate>();
            }
        }
        private AcsModuleRoleUpdate UpdateWorker
        {
            get
            {
                return (AcsModuleRoleUpdate)Worker.Get<AcsModuleRoleUpdate>();
            }
        }
        private AcsModuleRoleDelete DeleteWorker
        {
            get
            {
                return (AcsModuleRoleDelete)Worker.Get<AcsModuleRoleDelete>();
            }
        }
        private AcsModuleRoleTruncate TruncateWorker
        {
            get
            {
                return (AcsModuleRoleTruncate)Worker.Get<AcsModuleRoleTruncate>();
            }
        }
        private AcsModuleRoleGet GetWorker
        {
            get
            {
                return (AcsModuleRoleGet)Worker.Get<AcsModuleRoleGet>();
            }
        }
        private AcsModuleRoleCheck CheckWorker
        {
            get
            {
                return (AcsModuleRoleCheck)Worker.Get<AcsModuleRoleCheck>();
            }
        }

        public bool Create(ACS_MODULE_ROLE data)
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

        public bool CreateList(List<ACS_MODULE_ROLE> listData)
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

        public bool Update(ACS_MODULE_ROLE data)
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

        public bool UpdateList(List<ACS_MODULE_ROLE> listData)
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

        public bool Delete(ACS_MODULE_ROLE data)
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

        public bool DeleteList(List<ACS_MODULE_ROLE> listData)
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

        public bool Truncate(ACS_MODULE_ROLE data)
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

        public bool TruncateList(List<ACS_MODULE_ROLE> listData)
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

        public List<ACS_MODULE_ROLE> Get(AcsModuleRoleSO search, CommonParam param)
        {
            List<ACS_MODULE_ROLE> result = new List<ACS_MODULE_ROLE>();
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

        public ACS_MODULE_ROLE GetById(long id, AcsModuleRoleSO search)
        {
            ACS_MODULE_ROLE result = null;
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
