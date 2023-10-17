using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSeseTransReq
{
    public partial class HisSeseTransReqDAO : EntityBase
    {
        private HisSeseTransReqCreate CreateWorker
        {
            get
            {
                return (HisSeseTransReqCreate)Worker.Get<HisSeseTransReqCreate>();
            }
        }
        private HisSeseTransReqUpdate UpdateWorker
        {
            get
            {
                return (HisSeseTransReqUpdate)Worker.Get<HisSeseTransReqUpdate>();
            }
        }
        private HisSeseTransReqDelete DeleteWorker
        {
            get
            {
                return (HisSeseTransReqDelete)Worker.Get<HisSeseTransReqDelete>();
            }
        }
        private HisSeseTransReqTruncate TruncateWorker
        {
            get
            {
                return (HisSeseTransReqTruncate)Worker.Get<HisSeseTransReqTruncate>();
            }
        }
        private HisSeseTransReqGet GetWorker
        {
            get
            {
                return (HisSeseTransReqGet)Worker.Get<HisSeseTransReqGet>();
            }
        }
        private HisSeseTransReqCheck CheckWorker
        {
            get
            {
                return (HisSeseTransReqCheck)Worker.Get<HisSeseTransReqCheck>();
            }
        }

        public bool Create(HIS_SESE_TRANS_REQ data)
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

        public bool CreateList(List<HIS_SESE_TRANS_REQ> listData)
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

        public bool Update(HIS_SESE_TRANS_REQ data)
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

        public bool UpdateList(List<HIS_SESE_TRANS_REQ> listData)
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

        public bool Delete(HIS_SESE_TRANS_REQ data)
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

        public bool DeleteList(List<HIS_SESE_TRANS_REQ> listData)
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

        public bool Truncate(HIS_SESE_TRANS_REQ data)
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

        public bool TruncateList(List<HIS_SESE_TRANS_REQ> listData)
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

        public List<HIS_SESE_TRANS_REQ> Get(HisSeseTransReqSO search, CommonParam param)
        {
            List<HIS_SESE_TRANS_REQ> result = new List<HIS_SESE_TRANS_REQ>();
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

        public HIS_SESE_TRANS_REQ GetById(long id, HisSeseTransReqSO search)
        {
            HIS_SESE_TRANS_REQ result = null;
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
