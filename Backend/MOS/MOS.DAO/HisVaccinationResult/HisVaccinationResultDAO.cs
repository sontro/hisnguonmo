using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationResult
{
    public partial class HisVaccinationResultDAO : EntityBase
    {
        private HisVaccinationResultCreate CreateWorker
        {
            get
            {
                return (HisVaccinationResultCreate)Worker.Get<HisVaccinationResultCreate>();
            }
        }
        private HisVaccinationResultUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationResultUpdate)Worker.Get<HisVaccinationResultUpdate>();
            }
        }
        private HisVaccinationResultDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationResultDelete)Worker.Get<HisVaccinationResultDelete>();
            }
        }
        private HisVaccinationResultTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationResultTruncate)Worker.Get<HisVaccinationResultTruncate>();
            }
        }
        private HisVaccinationResultGet GetWorker
        {
            get
            {
                return (HisVaccinationResultGet)Worker.Get<HisVaccinationResultGet>();
            }
        }
        private HisVaccinationResultCheck CheckWorker
        {
            get
            {
                return (HisVaccinationResultCheck)Worker.Get<HisVaccinationResultCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_RESULT data)
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

        public bool CreateList(List<HIS_VACCINATION_RESULT> listData)
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

        public bool Update(HIS_VACCINATION_RESULT data)
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

        public bool UpdateList(List<HIS_VACCINATION_RESULT> listData)
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

        public bool Delete(HIS_VACCINATION_RESULT data)
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

        public bool DeleteList(List<HIS_VACCINATION_RESULT> listData)
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

        public bool Truncate(HIS_VACCINATION_RESULT data)
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

        public bool TruncateList(List<HIS_VACCINATION_RESULT> listData)
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

        public List<HIS_VACCINATION_RESULT> Get(HisVaccinationResultSO search, CommonParam param)
        {
            List<HIS_VACCINATION_RESULT> result = new List<HIS_VACCINATION_RESULT>();
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

        public HIS_VACCINATION_RESULT GetById(long id, HisVaccinationResultSO search)
        {
            HIS_VACCINATION_RESULT result = null;
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
