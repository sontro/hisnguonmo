using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqStt
{
    public partial class HisServiceReqSttDAO : EntityBase
    {
        private HisServiceReqSttCreate CreateWorker
        {
            get
            {
                return (HisServiceReqSttCreate)Worker.Get<HisServiceReqSttCreate>();
            }
        }
        private HisServiceReqSttUpdate UpdateWorker
        {
            get
            {
                return (HisServiceReqSttUpdate)Worker.Get<HisServiceReqSttUpdate>();
            }
        }
        private HisServiceReqSttDelete DeleteWorker
        {
            get
            {
                return (HisServiceReqSttDelete)Worker.Get<HisServiceReqSttDelete>();
            }
        }
        private HisServiceReqSttTruncate TruncateWorker
        {
            get
            {
                return (HisServiceReqSttTruncate)Worker.Get<HisServiceReqSttTruncate>();
            }
        }
        private HisServiceReqSttGet GetWorker
        {
            get
            {
                return (HisServiceReqSttGet)Worker.Get<HisServiceReqSttGet>();
            }
        }
        private HisServiceReqSttCheck CheckWorker
        {
            get
            {
                return (HisServiceReqSttCheck)Worker.Get<HisServiceReqSttCheck>();
            }
        }

        public bool Create(HIS_SERVICE_REQ_STT data)
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

        public bool CreateList(List<HIS_SERVICE_REQ_STT> listData)
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

        public bool Update(HIS_SERVICE_REQ_STT data)
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

        public bool UpdateList(List<HIS_SERVICE_REQ_STT> listData)
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

        public bool Delete(HIS_SERVICE_REQ_STT data)
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

        public bool DeleteList(List<HIS_SERVICE_REQ_STT> listData)
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

        public bool Truncate(HIS_SERVICE_REQ_STT data)
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

        public bool TruncateList(List<HIS_SERVICE_REQ_STT> listData)
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

        public List<HIS_SERVICE_REQ_STT> Get(HisServiceReqSttSO search, CommonParam param)
        {
            List<HIS_SERVICE_REQ_STT> result = new List<HIS_SERVICE_REQ_STT>();
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

        public HIS_SERVICE_REQ_STT GetById(long id, HisServiceReqSttSO search)
        {
            HIS_SERVICE_REQ_STT result = null;
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
