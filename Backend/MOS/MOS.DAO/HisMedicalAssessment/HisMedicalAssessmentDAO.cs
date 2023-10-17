using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicalAssessment
{
    public partial class HisMedicalAssessmentDAO : EntityBase
    {
        private HisMedicalAssessmentCreate CreateWorker
        {
            get
            {
                return (HisMedicalAssessmentCreate)Worker.Get<HisMedicalAssessmentCreate>();
            }
        }
        private HisMedicalAssessmentUpdate UpdateWorker
        {
            get
            {
                return (HisMedicalAssessmentUpdate)Worker.Get<HisMedicalAssessmentUpdate>();
            }
        }
        private HisMedicalAssessmentDelete DeleteWorker
        {
            get
            {
                return (HisMedicalAssessmentDelete)Worker.Get<HisMedicalAssessmentDelete>();
            }
        }
        private HisMedicalAssessmentTruncate TruncateWorker
        {
            get
            {
                return (HisMedicalAssessmentTruncate)Worker.Get<HisMedicalAssessmentTruncate>();
            }
        }
        private HisMedicalAssessmentGet GetWorker
        {
            get
            {
                return (HisMedicalAssessmentGet)Worker.Get<HisMedicalAssessmentGet>();
            }
        }
        private HisMedicalAssessmentCheck CheckWorker
        {
            get
            {
                return (HisMedicalAssessmentCheck)Worker.Get<HisMedicalAssessmentCheck>();
            }
        }

        public bool Create(HIS_MEDICAL_ASSESSMENT data)
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

        public bool CreateList(List<HIS_MEDICAL_ASSESSMENT> listData)
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

        public bool Update(HIS_MEDICAL_ASSESSMENT data)
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

        public bool UpdateList(List<HIS_MEDICAL_ASSESSMENT> listData)
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

        public bool Delete(HIS_MEDICAL_ASSESSMENT data)
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

        public bool DeleteList(List<HIS_MEDICAL_ASSESSMENT> listData)
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

        public bool Truncate(HIS_MEDICAL_ASSESSMENT data)
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

        public bool TruncateList(List<HIS_MEDICAL_ASSESSMENT> listData)
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

        public List<HIS_MEDICAL_ASSESSMENT> Get(HisMedicalAssessmentSO search, CommonParam param)
        {
            List<HIS_MEDICAL_ASSESSMENT> result = new List<HIS_MEDICAL_ASSESSMENT>();
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

        public HIS_MEDICAL_ASSESSMENT GetById(long id, HisMedicalAssessmentSO search)
        {
            HIS_MEDICAL_ASSESSMENT result = null;
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
