using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaHideControl
{
    public partial class SdaHideControlDAO : EntityBase
    {
        private SdaHideControlCreate CreateWorker
        {
            get
            {
                return (SdaHideControlCreate)Worker.Get<SdaHideControlCreate>();
            }
        }
        private SdaHideControlUpdate UpdateWorker
        {
            get
            {
                return (SdaHideControlUpdate)Worker.Get<SdaHideControlUpdate>();
            }
        }
        private SdaHideControlDelete DeleteWorker
        {
            get
            {
                return (SdaHideControlDelete)Worker.Get<SdaHideControlDelete>();
            }
        }
        private SdaHideControlTruncate TruncateWorker
        {
            get
            {
                return (SdaHideControlTruncate)Worker.Get<SdaHideControlTruncate>();
            }
        }
        private SdaHideControlGet GetWorker
        {
            get
            {
                return (SdaHideControlGet)Worker.Get<SdaHideControlGet>();
            }
        }
        private SdaHideControlCheck CheckWorker
        {
            get
            {
                return (SdaHideControlCheck)Worker.Get<SdaHideControlCheck>();
            }
        }

        public bool Create(SDA_HIDE_CONTROL data)
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

        public bool CreateList(List<SDA_HIDE_CONTROL> listData)
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

        public bool Update(SDA_HIDE_CONTROL data)
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

        public bool UpdateList(List<SDA_HIDE_CONTROL> listData)
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

        public bool Delete(SDA_HIDE_CONTROL data)
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

        public bool DeleteList(List<SDA_HIDE_CONTROL> listData)
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

        public bool Truncate(SDA_HIDE_CONTROL data)
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

        public bool TruncateList(List<SDA_HIDE_CONTROL> listData)
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

        public List<SDA_HIDE_CONTROL> Get(SdaHideControlSO search, CommonParam param)
        {
            List<SDA_HIDE_CONTROL> result = new List<SDA_HIDE_CONTROL>();
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

        public SDA_HIDE_CONTROL GetById(long id, SdaHideControlSO search)
        {
            SDA_HIDE_CONTROL result = null;
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
