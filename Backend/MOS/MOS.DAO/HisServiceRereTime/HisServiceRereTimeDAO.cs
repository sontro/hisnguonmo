using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRereTime
{
    public partial class HisServiceRereTimeDAO : EntityBase
    {
        private HisServiceRereTimeCreate CreateWorker
        {
            get
            {
                return (HisServiceRereTimeCreate)Worker.Get<HisServiceRereTimeCreate>();
            }
        }
        private HisServiceRereTimeUpdate UpdateWorker
        {
            get
            {
                return (HisServiceRereTimeUpdate)Worker.Get<HisServiceRereTimeUpdate>();
            }
        }
        private HisServiceRereTimeDelete DeleteWorker
        {
            get
            {
                return (HisServiceRereTimeDelete)Worker.Get<HisServiceRereTimeDelete>();
            }
        }
        private HisServiceRereTimeTruncate TruncateWorker
        {
            get
            {
                return (HisServiceRereTimeTruncate)Worker.Get<HisServiceRereTimeTruncate>();
            }
        }
        private HisServiceRereTimeGet GetWorker
        {
            get
            {
                return (HisServiceRereTimeGet)Worker.Get<HisServiceRereTimeGet>();
            }
        }
        private HisServiceRereTimeCheck CheckWorker
        {
            get
            {
                return (HisServiceRereTimeCheck)Worker.Get<HisServiceRereTimeCheck>();
            }
        }

        public bool Create(HIS_SERVICE_RERE_TIME data)
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

        public bool CreateList(List<HIS_SERVICE_RERE_TIME> listData)
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

        public bool Update(HIS_SERVICE_RERE_TIME data)
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

        public bool UpdateList(List<HIS_SERVICE_RERE_TIME> listData)
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

        public bool Delete(HIS_SERVICE_RERE_TIME data)
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

        public bool DeleteList(List<HIS_SERVICE_RERE_TIME> listData)
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

        public bool Truncate(HIS_SERVICE_RERE_TIME data)
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

        public bool TruncateList(List<HIS_SERVICE_RERE_TIME> listData)
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

        public List<HIS_SERVICE_RERE_TIME> Get(HisServiceRereTimeSO search, CommonParam param)
        {
            List<HIS_SERVICE_RERE_TIME> result = new List<HIS_SERVICE_RERE_TIME>();
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

        public HIS_SERVICE_RERE_TIME GetById(long id, HisServiceRereTimeSO search)
        {
            HIS_SERVICE_RERE_TIME result = null;
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
