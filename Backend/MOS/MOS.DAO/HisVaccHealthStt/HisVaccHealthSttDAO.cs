using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccHealthStt
{
    public partial class HisVaccHealthSttDAO : EntityBase
    {
        private HisVaccHealthSttCreate CreateWorker
        {
            get
            {
                return (HisVaccHealthSttCreate)Worker.Get<HisVaccHealthSttCreate>();
            }
        }
        private HisVaccHealthSttUpdate UpdateWorker
        {
            get
            {
                return (HisVaccHealthSttUpdate)Worker.Get<HisVaccHealthSttUpdate>();
            }
        }
        private HisVaccHealthSttDelete DeleteWorker
        {
            get
            {
                return (HisVaccHealthSttDelete)Worker.Get<HisVaccHealthSttDelete>();
            }
        }
        private HisVaccHealthSttTruncate TruncateWorker
        {
            get
            {
                return (HisVaccHealthSttTruncate)Worker.Get<HisVaccHealthSttTruncate>();
            }
        }
        private HisVaccHealthSttGet GetWorker
        {
            get
            {
                return (HisVaccHealthSttGet)Worker.Get<HisVaccHealthSttGet>();
            }
        }
        private HisVaccHealthSttCheck CheckWorker
        {
            get
            {
                return (HisVaccHealthSttCheck)Worker.Get<HisVaccHealthSttCheck>();
            }
        }

        public bool Create(HIS_VACC_HEALTH_STT data)
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

        public bool CreateList(List<HIS_VACC_HEALTH_STT> listData)
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

        public bool Update(HIS_VACC_HEALTH_STT data)
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

        public bool UpdateList(List<HIS_VACC_HEALTH_STT> listData)
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

        public bool Delete(HIS_VACC_HEALTH_STT data)
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

        public bool DeleteList(List<HIS_VACC_HEALTH_STT> listData)
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

        public bool Truncate(HIS_VACC_HEALTH_STT data)
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

        public bool TruncateList(List<HIS_VACC_HEALTH_STT> listData)
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

        public List<HIS_VACC_HEALTH_STT> Get(HisVaccHealthSttSO search, CommonParam param)
        {
            List<HIS_VACC_HEALTH_STT> result = new List<HIS_VACC_HEALTH_STT>();
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

        public HIS_VACC_HEALTH_STT GetById(long id, HisVaccHealthSttSO search)
        {
            HIS_VACC_HEALTH_STT result = null;
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
