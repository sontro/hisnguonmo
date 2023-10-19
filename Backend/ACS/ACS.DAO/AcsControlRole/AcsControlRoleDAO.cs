using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsControlRole
{
    public partial class AcsControlRoleDAO : EntityBase
    {
        private AcsControlRoleCreate CreateWorker
        {
            get
            {
                return (AcsControlRoleCreate)Worker.Get<AcsControlRoleCreate>();
            }
        }
        private AcsControlRoleUpdate UpdateWorker
        {
            get
            {
                return (AcsControlRoleUpdate)Worker.Get<AcsControlRoleUpdate>();
            }
        }
        private AcsControlRoleDelete DeleteWorker
        {
            get
            {
                return (AcsControlRoleDelete)Worker.Get<AcsControlRoleDelete>();
            }
        }
        private AcsControlRoleTruncate TruncateWorker
        {
            get
            {
                return (AcsControlRoleTruncate)Worker.Get<AcsControlRoleTruncate>();
            }
        }
        private AcsControlRoleGet GetWorker
        {
            get
            {
                return (AcsControlRoleGet)Worker.Get<AcsControlRoleGet>();
            }
        }
        private AcsControlRoleCheck CheckWorker
        {
            get
            {
                return (AcsControlRoleCheck)Worker.Get<AcsControlRoleCheck>();
            }
        }

        public bool Create(ACS_CONTROL_ROLE data)
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

        public bool CreateList(List<ACS_CONTROL_ROLE> listData)
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

        public bool Update(ACS_CONTROL_ROLE data)
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

        public bool UpdateList(List<ACS_CONTROL_ROLE> listData)
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

        public bool Delete(ACS_CONTROL_ROLE data)
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

        public bool DeleteList(List<ACS_CONTROL_ROLE> listData)
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

        public bool Truncate(ACS_CONTROL_ROLE data)
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

        public bool TruncateList(List<ACS_CONTROL_ROLE> listData)
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

        public List<ACS_CONTROL_ROLE> Get(AcsControlRoleSO search, CommonParam param)
        {
            List<ACS_CONTROL_ROLE> result = new List<ACS_CONTROL_ROLE>();
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

        public ACS_CONTROL_ROLE GetById(long id, AcsControlRoleSO search)
        {
            ACS_CONTROL_ROLE result = null;
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
