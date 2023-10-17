using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentLogType
{
    public partial class HisTreatmentLogTypeDAO : EntityBase
    {
        private HisTreatmentLogTypeCreate CreateWorker
        {
            get
            {
                return (HisTreatmentLogTypeCreate)Worker.Get<HisTreatmentLogTypeCreate>();
            }
        }
        private HisTreatmentLogTypeUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentLogTypeUpdate)Worker.Get<HisTreatmentLogTypeUpdate>();
            }
        }
        private HisTreatmentLogTypeDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentLogTypeDelete)Worker.Get<HisTreatmentLogTypeDelete>();
            }
        }
        private HisTreatmentLogTypeTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentLogTypeTruncate)Worker.Get<HisTreatmentLogTypeTruncate>();
            }
        }
        private HisTreatmentLogTypeGet GetWorker
        {
            get
            {
                return (HisTreatmentLogTypeGet)Worker.Get<HisTreatmentLogTypeGet>();
            }
        }
        private HisTreatmentLogTypeCheck CheckWorker
        {
            get
            {
                return (HisTreatmentLogTypeCheck)Worker.Get<HisTreatmentLogTypeCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_LOG_TYPE data)
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

        public bool CreateList(List<HIS_TREATMENT_LOG_TYPE> listData)
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

        public bool Update(HIS_TREATMENT_LOG_TYPE data)
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

        public bool UpdateList(List<HIS_TREATMENT_LOG_TYPE> listData)
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

        public bool Delete(HIS_TREATMENT_LOG_TYPE data)
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

        public bool DeleteList(List<HIS_TREATMENT_LOG_TYPE> listData)
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

        public bool Truncate(HIS_TREATMENT_LOG_TYPE data)
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

        public bool TruncateList(List<HIS_TREATMENT_LOG_TYPE> listData)
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

        public List<HIS_TREATMENT_LOG_TYPE> Get(HisTreatmentLogTypeSO search, CommonParam param)
        {
            List<HIS_TREATMENT_LOG_TYPE> result = new List<HIS_TREATMENT_LOG_TYPE>();
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

        public HIS_TREATMENT_LOG_TYPE GetById(long id, HisTreatmentLogTypeSO search)
        {
            HIS_TREATMENT_LOG_TYPE result = null;
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
