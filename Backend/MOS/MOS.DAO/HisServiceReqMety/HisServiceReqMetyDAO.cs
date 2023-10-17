using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMety
{
    public partial class HisServiceReqMetyDAO : EntityBase
    {
        private HisServiceReqMetyCreate CreateWorker
        {
            get
            {
                return (HisServiceReqMetyCreate)Worker.Get<HisServiceReqMetyCreate>();
            }
        }
        private HisServiceReqMetyUpdate UpdateWorker
        {
            get
            {
                return (HisServiceReqMetyUpdate)Worker.Get<HisServiceReqMetyUpdate>();
            }
        }
        private HisServiceReqMetyDelete DeleteWorker
        {
            get
            {
                return (HisServiceReqMetyDelete)Worker.Get<HisServiceReqMetyDelete>();
            }
        }
        private HisServiceReqMetyTruncate TruncateWorker
        {
            get
            {
                return (HisServiceReqMetyTruncate)Worker.Get<HisServiceReqMetyTruncate>();
            }
        }
        private HisServiceReqMetyGet GetWorker
        {
            get
            {
                return (HisServiceReqMetyGet)Worker.Get<HisServiceReqMetyGet>();
            }
        }
        private HisServiceReqMetyCheck CheckWorker
        {
            get
            {
                return (HisServiceReqMetyCheck)Worker.Get<HisServiceReqMetyCheck>();
            }
        }

        public bool Create(HIS_SERVICE_REQ_METY data)
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

        public bool CreateList(List<HIS_SERVICE_REQ_METY> listData)
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

        public bool Update(HIS_SERVICE_REQ_METY data)
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

        public bool UpdateList(List<HIS_SERVICE_REQ_METY> listData)
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

        public bool Delete(HIS_SERVICE_REQ_METY data)
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

        public bool DeleteList(List<HIS_SERVICE_REQ_METY> listData)
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

        public bool Truncate(HIS_SERVICE_REQ_METY data)
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

        public bool TruncateList(List<HIS_SERVICE_REQ_METY> listData)
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

        public List<HIS_SERVICE_REQ_METY> Get(HisServiceReqMetySO search, CommonParam param)
        {
            List<HIS_SERVICE_REQ_METY> result = new List<HIS_SERVICE_REQ_METY>();
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

        public HIS_SERVICE_REQ_METY GetById(long id, HisServiceReqMetySO search)
        {
            HIS_SERVICE_REQ_METY result = null;
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
