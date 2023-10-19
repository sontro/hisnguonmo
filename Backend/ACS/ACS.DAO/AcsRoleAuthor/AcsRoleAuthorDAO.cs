using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.DAO.AcsRoleAuthor
{
    public partial class AcsRoleAuthorDAO : EntityBase
    {
        private AcsRoleAuthorCreate CreateWorker
        {
            get
            {
                return (AcsRoleAuthorCreate)Worker.Get<AcsRoleAuthorCreate>();
            }
        }
        private AcsRoleAuthorUpdate UpdateWorker
        {
            get
            {
                return (AcsRoleAuthorUpdate)Worker.Get<AcsRoleAuthorUpdate>();
            }
        }
        private AcsRoleAuthorDelete DeleteWorker
        {
            get
            {
                return (AcsRoleAuthorDelete)Worker.Get<AcsRoleAuthorDelete>();
            }
        }
        private AcsRoleAuthorTruncate TruncateWorker
        {
            get
            {
                return (AcsRoleAuthorTruncate)Worker.Get<AcsRoleAuthorTruncate>();
            }
        }
        private AcsRoleAuthorGet GetWorker
        {
            get
            {
                return (AcsRoleAuthorGet)Worker.Get<AcsRoleAuthorGet>();
            }
        }
        private AcsRoleAuthorCheck CheckWorker
        {
            get
            {
                return (AcsRoleAuthorCheck)Worker.Get<AcsRoleAuthorCheck>();
            }
        }

        public bool Create(ACS_ROLE_AUTHOR data)
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

        public bool CreateList(List<ACS_ROLE_AUTHOR> listData)
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

        public bool Update(ACS_ROLE_AUTHOR data)
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

        public bool UpdateList(List<ACS_ROLE_AUTHOR> listData)
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

        public bool Delete(ACS_ROLE_AUTHOR data)
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

        public bool DeleteList(List<ACS_ROLE_AUTHOR> listData)
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

        public bool Truncate(ACS_ROLE_AUTHOR data)
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

        public bool TruncateList(List<ACS_ROLE_AUTHOR> listData)
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

        public List<ACS_ROLE_AUTHOR> Get(AcsRoleAuthorSO search, CommonParam param)
        {
            List<ACS_ROLE_AUTHOR> result = new List<ACS_ROLE_AUTHOR>();
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

        public ACS_ROLE_AUTHOR GetById(long id, AcsRoleAuthorSO search)
        {
            ACS_ROLE_AUTHOR result = null;
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
