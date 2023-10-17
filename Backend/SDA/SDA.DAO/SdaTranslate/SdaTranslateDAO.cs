using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaTranslate
{
    public partial class SdaTranslateDAO : EntityBase
    {
        private SdaTranslateCreate CreateWorker
        {
            get
            {
                return (SdaTranslateCreate)Worker.Get<SdaTranslateCreate>();
            }
        }
        private SdaTranslateUpdate UpdateWorker
        {
            get
            {
                return (SdaTranslateUpdate)Worker.Get<SdaTranslateUpdate>();
            }
        }
        private SdaTranslateDelete DeleteWorker
        {
            get
            {
                return (SdaTranslateDelete)Worker.Get<SdaTranslateDelete>();
            }
        }
        private SdaTranslateTruncate TruncateWorker
        {
            get
            {
                return (SdaTranslateTruncate)Worker.Get<SdaTranslateTruncate>();
            }
        }
        private SdaTranslateGet GetWorker
        {
            get
            {
                return (SdaTranslateGet)Worker.Get<SdaTranslateGet>();
            }
        }
        private SdaTranslateCheck CheckWorker
        {
            get
            {
                return (SdaTranslateCheck)Worker.Get<SdaTranslateCheck>();
            }
        }

        public bool Create(SDA_TRANSLATE data)
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

        public bool CreateList(List<SDA_TRANSLATE> listData)
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

        public bool Update(SDA_TRANSLATE data)
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

        public bool UpdateList(List<SDA_TRANSLATE> listData)
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

        public bool Delete(SDA_TRANSLATE data)
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

        public bool DeleteList(List<SDA_TRANSLATE> listData)
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

        public bool Truncate(SDA_TRANSLATE data)
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

        public bool TruncateList(List<SDA_TRANSLATE> listData)
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

        public List<SDA_TRANSLATE> Get(SdaTranslateSO search, CommonParam param)
        {
            List<SDA_TRANSLATE> result = new List<SDA_TRANSLATE>();
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

        public SDA_TRANSLATE GetById(long id, SdaTranslateSO search)
        {
            SDA_TRANSLATE result = null;
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

        public bool IsExistsCreate(SDA_TRANSLATE data)
        {
            try
            {
                return CheckWorker.IsExistsCreate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }

        public bool IsExistsUpdate(SDA_TRANSLATE data)
        {
            try
            {
                return CheckWorker.IsExistsUpdate(data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
