using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMatyReqDt
{
    public partial class HisBcsMatyReqDtDAO : EntityBase
    {
        private HisBcsMatyReqDtCreate CreateWorker
        {
            get
            {
                return (HisBcsMatyReqDtCreate)Worker.Get<HisBcsMatyReqDtCreate>();
            }
        }
        private HisBcsMatyReqDtUpdate UpdateWorker
        {
            get
            {
                return (HisBcsMatyReqDtUpdate)Worker.Get<HisBcsMatyReqDtUpdate>();
            }
        }
        private HisBcsMatyReqDtDelete DeleteWorker
        {
            get
            {
                return (HisBcsMatyReqDtDelete)Worker.Get<HisBcsMatyReqDtDelete>();
            }
        }
        private HisBcsMatyReqDtTruncate TruncateWorker
        {
            get
            {
                return (HisBcsMatyReqDtTruncate)Worker.Get<HisBcsMatyReqDtTruncate>();
            }
        }
        private HisBcsMatyReqDtGet GetWorker
        {
            get
            {
                return (HisBcsMatyReqDtGet)Worker.Get<HisBcsMatyReqDtGet>();
            }
        }
        private HisBcsMatyReqDtCheck CheckWorker
        {
            get
            {
                return (HisBcsMatyReqDtCheck)Worker.Get<HisBcsMatyReqDtCheck>();
            }
        }

        public bool Create(HIS_BCS_MATY_REQ_DT data)
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

        public bool CreateList(List<HIS_BCS_MATY_REQ_DT> listData)
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

        public bool Update(HIS_BCS_MATY_REQ_DT data)
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

        public bool UpdateList(List<HIS_BCS_MATY_REQ_DT> listData)
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

        public bool Delete(HIS_BCS_MATY_REQ_DT data)
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

        public bool DeleteList(List<HIS_BCS_MATY_REQ_DT> listData)
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

        public bool Truncate(HIS_BCS_MATY_REQ_DT data)
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

        public bool TruncateList(List<HIS_BCS_MATY_REQ_DT> listData)
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

        public List<HIS_BCS_MATY_REQ_DT> Get(HisBcsMatyReqDtSO search, CommonParam param)
        {
            List<HIS_BCS_MATY_REQ_DT> result = new List<HIS_BCS_MATY_REQ_DT>();
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

        public HIS_BCS_MATY_REQ_DT GetById(long id, HisBcsMatyReqDtSO search)
        {
            HIS_BCS_MATY_REQ_DT result = null;
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
