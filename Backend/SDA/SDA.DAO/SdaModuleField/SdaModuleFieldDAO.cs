using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaModuleField
{
    public partial class SdaModuleFieldDAO : EntityBase
    {
        private SdaModuleFieldCreate CreateWorker
        {
            get
            {
                return (SdaModuleFieldCreate)Worker.Get<SdaModuleFieldCreate>();
            }
        }
        private SdaModuleFieldUpdate UpdateWorker
        {
            get
            {
                return (SdaModuleFieldUpdate)Worker.Get<SdaModuleFieldUpdate>();
            }
        }
        private SdaModuleFieldDelete DeleteWorker
        {
            get
            {
                return (SdaModuleFieldDelete)Worker.Get<SdaModuleFieldDelete>();
            }
        }
        private SdaModuleFieldTruncate TruncateWorker
        {
            get
            {
                return (SdaModuleFieldTruncate)Worker.Get<SdaModuleFieldTruncate>();
            }
        }
        private SdaModuleFieldGet GetWorker
        {
            get
            {
                return (SdaModuleFieldGet)Worker.Get<SdaModuleFieldGet>();
            }
        }
        private SdaModuleFieldCheck CheckWorker
        {
            get
            {
                return (SdaModuleFieldCheck)Worker.Get<SdaModuleFieldCheck>();
            }
        }

        public bool Create(SDA_MODULE_FIELD data)
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

        public bool CreateList(List<SDA_MODULE_FIELD> listData)
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

        public bool Update(SDA_MODULE_FIELD data)
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

        public bool UpdateList(List<SDA_MODULE_FIELD> listData)
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

        public bool Delete(SDA_MODULE_FIELD data)
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

        public bool DeleteList(List<SDA_MODULE_FIELD> listData)
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

        public bool Truncate(SDA_MODULE_FIELD data)
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

        public bool TruncateList(List<SDA_MODULE_FIELD> listData)
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

        public List<SDA_MODULE_FIELD> Get(SdaModuleFieldSO search, CommonParam param)
        {
            List<SDA_MODULE_FIELD> result = new List<SDA_MODULE_FIELD>();
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

        public SDA_MODULE_FIELD GetById(long id, SdaModuleFieldSO search)
        {
            SDA_MODULE_FIELD result = null;
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
