using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentResult
{
    public partial class HisTreatmentResultDAO : EntityBase
    {
        private HisTreatmentResultCreate CreateWorker
        {
            get
            {
                return (HisTreatmentResultCreate)Worker.Get<HisTreatmentResultCreate>();
            }
        }
        private HisTreatmentResultUpdate UpdateWorker
        {
            get
            {
                return (HisTreatmentResultUpdate)Worker.Get<HisTreatmentResultUpdate>();
            }
        }
        private HisTreatmentResultDelete DeleteWorker
        {
            get
            {
                return (HisTreatmentResultDelete)Worker.Get<HisTreatmentResultDelete>();
            }
        }
        private HisTreatmentResultTruncate TruncateWorker
        {
            get
            {
                return (HisTreatmentResultTruncate)Worker.Get<HisTreatmentResultTruncate>();
            }
        }
        private HisTreatmentResultGet GetWorker
        {
            get
            {
                return (HisTreatmentResultGet)Worker.Get<HisTreatmentResultGet>();
            }
        }
        private HisTreatmentResultCheck CheckWorker
        {
            get
            {
                return (HisTreatmentResultCheck)Worker.Get<HisTreatmentResultCheck>();
            }
        }

        public bool Create(HIS_TREATMENT_RESULT data)
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

        public bool CreateList(List<HIS_TREATMENT_RESULT> listData)
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

        public bool Update(HIS_TREATMENT_RESULT data)
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

        public bool UpdateList(List<HIS_TREATMENT_RESULT> listData)
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

        public bool Delete(HIS_TREATMENT_RESULT data)
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

        public bool DeleteList(List<HIS_TREATMENT_RESULT> listData)
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

        public bool Truncate(HIS_TREATMENT_RESULT data)
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

        public bool TruncateList(List<HIS_TREATMENT_RESULT> listData)
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

        public List<HIS_TREATMENT_RESULT> Get(HisTreatmentResultSO search, CommonParam param)
        {
            List<HIS_TREATMENT_RESULT> result = new List<HIS_TREATMENT_RESULT>();
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

        public HIS_TREATMENT_RESULT GetById(long id, HisTreatmentResultSO search)
        {
            HIS_TREATMENT_RESULT result = null;
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
