using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPatyTrty
{
    public partial class HisMestPatyTrtyDAO : EntityBase
    {
        private HisMestPatyTrtyCreate CreateWorker
        {
            get
            {
                return (HisMestPatyTrtyCreate)Worker.Get<HisMestPatyTrtyCreate>();
            }
        }
        private HisMestPatyTrtyUpdate UpdateWorker
        {
            get
            {
                return (HisMestPatyTrtyUpdate)Worker.Get<HisMestPatyTrtyUpdate>();
            }
        }
        private HisMestPatyTrtyDelete DeleteWorker
        {
            get
            {
                return (HisMestPatyTrtyDelete)Worker.Get<HisMestPatyTrtyDelete>();
            }
        }
        private HisMestPatyTrtyTruncate TruncateWorker
        {
            get
            {
                return (HisMestPatyTrtyTruncate)Worker.Get<HisMestPatyTrtyTruncate>();
            }
        }
        private HisMestPatyTrtyGet GetWorker
        {
            get
            {
                return (HisMestPatyTrtyGet)Worker.Get<HisMestPatyTrtyGet>();
            }
        }
        private HisMestPatyTrtyCheck CheckWorker
        {
            get
            {
                return (HisMestPatyTrtyCheck)Worker.Get<HisMestPatyTrtyCheck>();
            }
        }

        public bool Create(HIS_MEST_PATY_TRTY data)
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

        public bool CreateList(List<HIS_MEST_PATY_TRTY> listData)
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

        public bool Update(HIS_MEST_PATY_TRTY data)
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

        public bool UpdateList(List<HIS_MEST_PATY_TRTY> listData)
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

        public bool Delete(HIS_MEST_PATY_TRTY data)
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

        public bool DeleteList(List<HIS_MEST_PATY_TRTY> listData)
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

        public bool Truncate(HIS_MEST_PATY_TRTY data)
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

        public bool TruncateList(List<HIS_MEST_PATY_TRTY> listData)
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

        public List<HIS_MEST_PATY_TRTY> Get(HisMestPatyTrtySO search, CommonParam param)
        {
            List<HIS_MEST_PATY_TRTY> result = new List<HIS_MEST_PATY_TRTY>();
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

        public HIS_MEST_PATY_TRTY GetById(long id, HisMestPatyTrtySO search)
        {
            HIS_MEST_PATY_TRTY result = null;
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
