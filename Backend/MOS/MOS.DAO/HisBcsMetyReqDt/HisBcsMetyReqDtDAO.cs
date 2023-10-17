using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMetyReqDt
{
    public partial class HisBcsMetyReqDtDAO : EntityBase
    {
        private HisBcsMetyReqDtCreate CreateWorker
        {
            get
            {
                return (HisBcsMetyReqDtCreate)Worker.Get<HisBcsMetyReqDtCreate>();
            }
        }
        private HisBcsMetyReqDtUpdate UpdateWorker
        {
            get
            {
                return (HisBcsMetyReqDtUpdate)Worker.Get<HisBcsMetyReqDtUpdate>();
            }
        }
        private HisBcsMetyReqDtDelete DeleteWorker
        {
            get
            {
                return (HisBcsMetyReqDtDelete)Worker.Get<HisBcsMetyReqDtDelete>();
            }
        }
        private HisBcsMetyReqDtTruncate TruncateWorker
        {
            get
            {
                return (HisBcsMetyReqDtTruncate)Worker.Get<HisBcsMetyReqDtTruncate>();
            }
        }
        private HisBcsMetyReqDtGet GetWorker
        {
            get
            {
                return (HisBcsMetyReqDtGet)Worker.Get<HisBcsMetyReqDtGet>();
            }
        }
        private HisBcsMetyReqDtCheck CheckWorker
        {
            get
            {
                return (HisBcsMetyReqDtCheck)Worker.Get<HisBcsMetyReqDtCheck>();
            }
        }

        public bool Create(HIS_BCS_METY_REQ_DT data)
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

        public bool CreateList(List<HIS_BCS_METY_REQ_DT> listData)
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

        public bool Update(HIS_BCS_METY_REQ_DT data)
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

        public bool UpdateList(List<HIS_BCS_METY_REQ_DT> listData)
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

        public bool Delete(HIS_BCS_METY_REQ_DT data)
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

        public bool DeleteList(List<HIS_BCS_METY_REQ_DT> listData)
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

        public bool Truncate(HIS_BCS_METY_REQ_DT data)
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

        public bool TruncateList(List<HIS_BCS_METY_REQ_DT> listData)
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

        public List<HIS_BCS_METY_REQ_DT> Get(HisBcsMetyReqDtSO search, CommonParam param)
        {
            List<HIS_BCS_METY_REQ_DT> result = new List<HIS_BCS_METY_REQ_DT>();
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

        public HIS_BCS_METY_REQ_DT GetById(long id, HisBcsMetyReqDtSO search)
        {
            HIS_BCS_METY_REQ_DT result = null;
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
