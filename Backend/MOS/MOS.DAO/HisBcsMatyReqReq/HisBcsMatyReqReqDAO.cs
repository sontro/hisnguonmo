using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMatyReqReq
{
    public partial class HisBcsMatyReqReqDAO : EntityBase
    {
        private HisBcsMatyReqReqCreate CreateWorker
        {
            get
            {
                return (HisBcsMatyReqReqCreate)Worker.Get<HisBcsMatyReqReqCreate>();
            }
        }
        private HisBcsMatyReqReqUpdate UpdateWorker
        {
            get
            {
                return (HisBcsMatyReqReqUpdate)Worker.Get<HisBcsMatyReqReqUpdate>();
            }
        }
        private HisBcsMatyReqReqDelete DeleteWorker
        {
            get
            {
                return (HisBcsMatyReqReqDelete)Worker.Get<HisBcsMatyReqReqDelete>();
            }
        }
        private HisBcsMatyReqReqTruncate TruncateWorker
        {
            get
            {
                return (HisBcsMatyReqReqTruncate)Worker.Get<HisBcsMatyReqReqTruncate>();
            }
        }
        private HisBcsMatyReqReqGet GetWorker
        {
            get
            {
                return (HisBcsMatyReqReqGet)Worker.Get<HisBcsMatyReqReqGet>();
            }
        }
        private HisBcsMatyReqReqCheck CheckWorker
        {
            get
            {
                return (HisBcsMatyReqReqCheck)Worker.Get<HisBcsMatyReqReqCheck>();
            }
        }

        public bool Create(HIS_BCS_MATY_REQ_REQ data)
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

        public bool CreateList(List<HIS_BCS_MATY_REQ_REQ> listData)
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

        public bool Update(HIS_BCS_MATY_REQ_REQ data)
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

        public bool UpdateList(List<HIS_BCS_MATY_REQ_REQ> listData)
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

        public bool Delete(HIS_BCS_MATY_REQ_REQ data)
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

        public bool DeleteList(List<HIS_BCS_MATY_REQ_REQ> listData)
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

        public bool Truncate(HIS_BCS_MATY_REQ_REQ data)
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

        public bool TruncateList(List<HIS_BCS_MATY_REQ_REQ> listData)
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

        public List<HIS_BCS_MATY_REQ_REQ> Get(HisBcsMatyReqReqSO search, CommonParam param)
        {
            List<HIS_BCS_MATY_REQ_REQ> result = new List<HIS_BCS_MATY_REQ_REQ>();
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

        public HIS_BCS_MATY_REQ_REQ GetById(long id, HisBcsMatyReqReqSO search)
        {
            HIS_BCS_MATY_REQ_REQ result = null;
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
