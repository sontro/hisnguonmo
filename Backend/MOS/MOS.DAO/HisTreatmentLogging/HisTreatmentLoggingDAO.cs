using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentLogging
{
    public partial class HisTreatmentLoggingDAO : EntityBase
    {
        private HisTreatmentLoggingCreate CreateWorker
        {
            get
            {
                return (HisTreatmentLoggingCreate)Worker.Get<HisTreatmentLoggingCreate>();
            }
        }
        private HisTreatmentLoggingUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentLoggingUpdate)Worker.Get<HisTreatmentLoggingUpdate>();
            }
        }
        private HisTreatmentLoggingDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentLoggingDelete)Worker.Get<HisTreatmentLoggingDelete>();
            }
        }
        private HisTreatmentLoggingTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentLoggingTruncate)Worker.Get<HisTreatmentLoggingTruncate>();
            }
        }
        private HisTreatmentLoggingGet GetWorker
        {
            get
            {
                return (HisTreatmentLoggingGet)Worker.Get<HisTreatmentLoggingGet>();
            }
        }
        private HisTreatmentLoggingCheck CheckWorker
        {
            get
            {
                return (HisTreatmentLoggingCheck)Worker.Get<HisTreatmentLoggingCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_LOGGING data)
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

        public bool CreateList(List<HIS_TREATMENT_LOGGING> listData)
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

        public bool Update(HIS_TREATMENT_LOGGING data)
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

        public bool UpdateList(List<HIS_TREATMENT_LOGGING> listData)
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

        public bool Delete(HIS_TREATMENT_LOGGING data)
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

        public bool DeleteList(List<HIS_TREATMENT_LOGGING> listData)
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

        public bool Truncate(HIS_TREATMENT_LOGGING data)
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

        public bool TruncateList(List<HIS_TREATMENT_LOGGING> listData)
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

        public List<HIS_TREATMENT_LOGGING> Get(HisTreatmentLoggingSO search, CommonParam param)
        {
            List<HIS_TREATMENT_LOGGING> result = new List<HIS_TREATMENT_LOGGING>();
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

        public HIS_TREATMENT_LOGGING GetById(long id, HisTreatmentLoggingSO search)
        {
            HIS_TREATMENT_LOGGING result = null;
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
