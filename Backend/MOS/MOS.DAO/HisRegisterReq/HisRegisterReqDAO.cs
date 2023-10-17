using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterReq
{
    public partial class HisRegisterReqDAO : EntityBase
    {
        private HisRegisterReqCreate CreateWorker
        {
            get
            {
                return (HisRegisterReqCreate)Worker.Get<HisRegisterReqCreate>();
            }
        }
        private HisRegisterReqUpdate UpdateWorker
        {
            get
            {
                return (HisRegisterReqUpdate)Worker.Get<HisRegisterReqUpdate>();
            }
        }
        private HisRegisterReqDelete DeleteWorker
        {
            get
            {
                return (HisRegisterReqDelete)Worker.Get<HisRegisterReqDelete>();
            }
        }
        private HisRegisterReqTruncate TruncateWorker
        {
            get
            {
                return (HisRegisterReqTruncate)Worker.Get<HisRegisterReqTruncate>();
            }
        }
        private HisRegisterReqGet GetWorker
        {
            get
            {
                return (HisRegisterReqGet)Worker.Get<HisRegisterReqGet>();
            }
        }
        private HisRegisterReqCheck CheckWorker
        {
            get
            {
                return (HisRegisterReqCheck)Worker.Get<HisRegisterReqCheck>();
            }
        }

        public bool Create(HIS_REGISTER_REQ data)
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

        public bool CreateList(List<HIS_REGISTER_REQ> listData)
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

        public bool Update(HIS_REGISTER_REQ data)
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

        public bool UpdateList(List<HIS_REGISTER_REQ> listData)
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

        public bool Delete(HIS_REGISTER_REQ data)
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

        public bool DeleteList(List<HIS_REGISTER_REQ> listData)
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

        public bool Truncate(HIS_REGISTER_REQ data)
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

        public bool TruncateList(List<HIS_REGISTER_REQ> listData)
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

        public List<HIS_REGISTER_REQ> Get(HisRegisterReqSO search, CommonParam param)
        {
            List<HIS_REGISTER_REQ> result = new List<HIS_REGISTER_REQ>();
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

        public HIS_REGISTER_REQ GetById(long id, HisRegisterReqSO search)
        {
            HIS_REGISTER_REQ result = null;
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
