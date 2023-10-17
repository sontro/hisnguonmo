using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReqMaty
{
    public partial class HisServiceReqMatyDAO : EntityBase
    {
        private HisServiceReqMatyCreate CreateWorker
        {
            get
            {
                return (HisServiceReqMatyCreate)Worker.Get<HisServiceReqMatyCreate>();
            }
        }
        private HisServiceReqMatyUpdate UpdateWorker
        {
            get
            {
                return (HisServiceReqMatyUpdate)Worker.Get<HisServiceReqMatyUpdate>();
            }
        }
        private HisServiceReqMatyDelete DeleteWorker
        {
            get
            {
                return (HisServiceReqMatyDelete)Worker.Get<HisServiceReqMatyDelete>();
            }
        }
        private HisServiceReqMatyTruncate TruncateWorker
        {
            get
            {
                return (HisServiceReqMatyTruncate)Worker.Get<HisServiceReqMatyTruncate>();
            }
        }
        private HisServiceReqMatyGet GetWorker
        {
            get
            {
                return (HisServiceReqMatyGet)Worker.Get<HisServiceReqMatyGet>();
            }
        }
        private HisServiceReqMatyCheck CheckWorker
        {
            get
            {
                return (HisServiceReqMatyCheck)Worker.Get<HisServiceReqMatyCheck>();
            }
        }

        public bool Create(HIS_SERVICE_REQ_MATY data)
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

        public bool CreateList(List<HIS_SERVICE_REQ_MATY> listData)
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

        public bool Update(HIS_SERVICE_REQ_MATY data)
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

        public bool UpdateList(List<HIS_SERVICE_REQ_MATY> listData)
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

        public bool Delete(HIS_SERVICE_REQ_MATY data)
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

        public bool DeleteList(List<HIS_SERVICE_REQ_MATY> listData)
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

        public bool Truncate(HIS_SERVICE_REQ_MATY data)
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

        public bool TruncateList(List<HIS_SERVICE_REQ_MATY> listData)
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

        public List<HIS_SERVICE_REQ_MATY> Get(HisServiceReqMatySO search, CommonParam param)
        {
            List<HIS_SERVICE_REQ_MATY> result = new List<HIS_SERVICE_REQ_MATY>();
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

        public HIS_SERVICE_REQ_MATY GetById(long id, HisServiceReqMatySO search)
        {
            HIS_SERVICE_REQ_MATY result = null;
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
