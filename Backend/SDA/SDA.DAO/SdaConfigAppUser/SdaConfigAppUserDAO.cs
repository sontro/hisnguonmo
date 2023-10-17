using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfigAppUser
{
    public partial class SdaConfigAppUserDAO : EntityBase
    {
        private SdaConfigAppUserCreate CreateWorker
        {
            get
            {
                return (SdaConfigAppUserCreate)Worker.Get<SdaConfigAppUserCreate>();
            }
        }
        private SdaConfigAppUserUpdate UpdateWorker
        {
            get
            {
                return (SdaConfigAppUserUpdate)Worker.Get<SdaConfigAppUserUpdate>();
            }
        }
        private SdaConfigAppUserDelete DeleteWorker
        {
            get
            {
                return (SdaConfigAppUserDelete)Worker.Get<SdaConfigAppUserDelete>();
            }
        }
        private SdaConfigAppUserTruncate TruncateWorker
        {
            get
            {
                return (SdaConfigAppUserTruncate)Worker.Get<SdaConfigAppUserTruncate>();
            }
        }
        private SdaConfigAppUserGet GetWorker
        {
            get
            {
                return (SdaConfigAppUserGet)Worker.Get<SdaConfigAppUserGet>();
            }
        }
        private SdaConfigAppUserCheck CheckWorker
        {
            get
            {
                return (SdaConfigAppUserCheck)Worker.Get<SdaConfigAppUserCheck>();
            }
        }

        public bool Create(SDA_CONFIG_APP_USER data)
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

        public bool CreateList(List<SDA_CONFIG_APP_USER> listData)
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

        public bool Update(SDA_CONFIG_APP_USER data)
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

        public bool UpdateList(List<SDA_CONFIG_APP_USER> listData)
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

        public bool Delete(SDA_CONFIG_APP_USER data)
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

        public bool DeleteList(List<SDA_CONFIG_APP_USER> listData)
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

        public bool Truncate(SDA_CONFIG_APP_USER data)
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

        public bool TruncateList(List<SDA_CONFIG_APP_USER> listData)
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

        public List<SDA_CONFIG_APP_USER> Get(SdaConfigAppUserSO search, CommonParam param)
        {
            List<SDA_CONFIG_APP_USER> result = new List<SDA_CONFIG_APP_USER>();
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

        public SDA_CONFIG_APP_USER GetById(long id, SdaConfigAppUserSO search)
        {
            SDA_CONFIG_APP_USER result = null;
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
