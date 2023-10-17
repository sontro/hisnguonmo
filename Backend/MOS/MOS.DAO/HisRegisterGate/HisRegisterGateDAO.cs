using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisRegisterGate
{
    public partial class HisRegisterGateDAO : EntityBase
    {
        private HisRegisterGateCreate CreateWorker
        {
            get
            {
                return (HisRegisterGateCreate)Worker.Get<HisRegisterGateCreate>();
            }
        }
        private HisRegisterGateUpdate UpdateWorker
        {
            get
            {
                return (HisRegisterGateUpdate)Worker.Get<HisRegisterGateUpdate>();
            }
        }
        private HisRegisterGateDelete DeleteWorker
        {
            get
            {
                return (HisRegisterGateDelete)Worker.Get<HisRegisterGateDelete>();
            }
        }
        private HisRegisterGateTruncate TruncateWorker
        {
            get
            {
                return (HisRegisterGateTruncate)Worker.Get<HisRegisterGateTruncate>();
            }
        }
        private HisRegisterGateGet GetWorker
        {
            get
            {
                return (HisRegisterGateGet)Worker.Get<HisRegisterGateGet>();
            }
        }
        private HisRegisterGateCheck CheckWorker
        {
            get
            {
                return (HisRegisterGateCheck)Worker.Get<HisRegisterGateCheck>();
            }
        }

        public bool Create(HIS_REGISTER_GATE data)
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

        public bool CreateList(List<HIS_REGISTER_GATE> listData)
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

        public bool Update(HIS_REGISTER_GATE data)
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

        public bool UpdateList(List<HIS_REGISTER_GATE> listData)
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

        public bool Delete(HIS_REGISTER_GATE data)
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

        public bool DeleteList(List<HIS_REGISTER_GATE> listData)
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

        public bool Truncate(HIS_REGISTER_GATE data)
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

        public bool TruncateList(List<HIS_REGISTER_GATE> listData)
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

        public List<HIS_REGISTER_GATE> Get(HisRegisterGateSO search, CommonParam param)
        {
            List<HIS_REGISTER_GATE> result = new List<HIS_REGISTER_GATE>();
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

        public HIS_REGISTER_GATE GetById(long id, HisRegisterGateSO search)
        {
            HIS_REGISTER_GATE result = null;
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
