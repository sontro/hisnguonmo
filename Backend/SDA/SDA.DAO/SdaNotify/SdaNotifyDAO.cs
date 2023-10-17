using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaNotify
{
    public partial class SdaNotifyDAO : EntityBase
    {
        private SdaNotifyCreate CreateWorker
        {
            get
            {
                return (SdaNotifyCreate)Worker.Get<SdaNotifyCreate>();
            }
        }
        private SdaNotifyUpdate UpdateWorker
        {
            get
            {
                return (SdaNotifyUpdate)Worker.Get<SdaNotifyUpdate>();
            }
        }
        private SdaNotifyDelete DeleteWorker
        {
            get
            {
                return (SdaNotifyDelete)Worker.Get<SdaNotifyDelete>();
            }
        }
        private SdaNotifyTruncate TruncateWorker
        {
            get
            {
                return (SdaNotifyTruncate)Worker.Get<SdaNotifyTruncate>();
            }
        }
        private SdaNotifyGet GetWorker
        {
            get
            {
                return (SdaNotifyGet)Worker.Get<SdaNotifyGet>();
            }
        }
        private SdaNotifyCheck CheckWorker
        {
            get
            {
                return (SdaNotifyCheck)Worker.Get<SdaNotifyCheck>();
            }
        }

        public bool Create(SDA_NOTIFY data)
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

        public bool CreateList(List<SDA_NOTIFY> listData)
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

        public bool Update(SDA_NOTIFY data)
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

        public bool UpdateList(List<SDA_NOTIFY> listData)
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

        public bool Delete(SDA_NOTIFY data)
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

        public bool DeleteList(List<SDA_NOTIFY> listData)
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

        public bool Truncate(SDA_NOTIFY data)
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

        public bool TruncateList(List<SDA_NOTIFY> listData)
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

        public List<SDA_NOTIFY> Get(SdaNotifySO search, CommonParam param)
        {
            List<SDA_NOTIFY> result = new List<SDA_NOTIFY>();
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

        public SDA_NOTIFY GetById(long id, SdaNotifySO search)
        {
            SDA_NOTIFY result = null;
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
