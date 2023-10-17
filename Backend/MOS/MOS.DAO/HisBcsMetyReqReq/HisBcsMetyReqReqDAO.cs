using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMetyReqReq
{
    public partial class HisBcsMetyReqReqDAO : EntityBase
    {
        private HisBcsMetyReqReqCreate CreateWorker
        {
            get
            {
                return (HisBcsMetyReqReqCreate)Worker.Get<HisBcsMetyReqReqCreate>();
            }
        }
        private HisBcsMetyReqReqUpdate UpdateWorker
        {
            get
            {
                return (HisBcsMetyReqReqUpdate)Worker.Get<HisBcsMetyReqReqUpdate>();
            }
        }
        private HisBcsMetyReqReqDelete DeleteWorker
        {
            get
            {
                return (HisBcsMetyReqReqDelete)Worker.Get<HisBcsMetyReqReqDelete>();
            }
        }
        private HisBcsMetyReqReqTruncate TruncateWorker
        {
            get
            {
                return (HisBcsMetyReqReqTruncate)Worker.Get<HisBcsMetyReqReqTruncate>();
            }
        }
        private HisBcsMetyReqReqGet GetWorker
        {
            get
            {
                return (HisBcsMetyReqReqGet)Worker.Get<HisBcsMetyReqReqGet>();
            }
        }
        private HisBcsMetyReqReqCheck CheckWorker
        {
            get
            {
                return (HisBcsMetyReqReqCheck)Worker.Get<HisBcsMetyReqReqCheck>();
            }
        }

        public bool Create(HIS_BCS_METY_REQ_REQ data)
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

        public bool CreateList(List<HIS_BCS_METY_REQ_REQ> listData)
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

        public bool Update(HIS_BCS_METY_REQ_REQ data)
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

        public bool UpdateList(List<HIS_BCS_METY_REQ_REQ> listData)
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

        public bool Delete(HIS_BCS_METY_REQ_REQ data)
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

        public bool DeleteList(List<HIS_BCS_METY_REQ_REQ> listData)
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

        public bool Truncate(HIS_BCS_METY_REQ_REQ data)
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

        public bool TruncateList(List<HIS_BCS_METY_REQ_REQ> listData)
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

        public List<HIS_BCS_METY_REQ_REQ> Get(HisBcsMetyReqReqSO search, CommonParam param)
        {
            List<HIS_BCS_METY_REQ_REQ> result = new List<HIS_BCS_METY_REQ_REQ>();
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

        public HIS_BCS_METY_REQ_REQ GetById(long id, HisBcsMetyReqReqSO search)
        {
            HIS_BCS_METY_REQ_REQ result = null;
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
