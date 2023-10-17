using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationStt
{
    public partial class HisVaccinationSttDAO : EntityBase
    {
        private HisVaccinationSttCreate CreateWorker
        {
            get
            {
                return (HisVaccinationSttCreate)Worker.Get<HisVaccinationSttCreate>();
            }
        }
        private HisVaccinationSttUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationSttUpdate)Worker.Get<HisVaccinationSttUpdate>();
            }
        }
        private HisVaccinationSttDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationSttDelete)Worker.Get<HisVaccinationSttDelete>();
            }
        }
        private HisVaccinationSttTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationSttTruncate)Worker.Get<HisVaccinationSttTruncate>();
            }
        }
        private HisVaccinationSttGet GetWorker
        {
            get
            {
                return (HisVaccinationSttGet)Worker.Get<HisVaccinationSttGet>();
            }
        }
        private HisVaccinationSttCheck CheckWorker
        {
            get
            {
                return (HisVaccinationSttCheck)Worker.Get<HisVaccinationSttCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_STT data)
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

        public bool CreateList(List<HIS_VACCINATION_STT> listData)
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

        public bool Update(HIS_VACCINATION_STT data)
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

        public bool UpdateList(List<HIS_VACCINATION_STT> listData)
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

        public bool Delete(HIS_VACCINATION_STT data)
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

        public bool DeleteList(List<HIS_VACCINATION_STT> listData)
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

        public bool Truncate(HIS_VACCINATION_STT data)
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

        public bool TruncateList(List<HIS_VACCINATION_STT> listData)
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

        public List<HIS_VACCINATION_STT> Get(HisVaccinationSttSO search, CommonParam param)
        {
            List<HIS_VACCINATION_STT> result = new List<HIS_VACCINATION_STT>();
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

        public HIS_VACCINATION_STT GetById(long id, HisVaccinationSttSO search)
        {
            HIS_VACCINATION_STT result = null;
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
