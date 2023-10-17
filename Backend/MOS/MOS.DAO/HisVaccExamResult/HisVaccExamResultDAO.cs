using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccExamResult
{
    public partial class HisVaccExamResultDAO : EntityBase
    {
        private HisVaccExamResultCreate CreateWorker
        {
            get
            {
                return (HisVaccExamResultCreate)Worker.Get<HisVaccExamResultCreate>();
            }
        }
        private HisVaccExamResultUpdate UpdateWorker
        {
            get
            {
                return (HisVaccExamResultUpdate)Worker.Get<HisVaccExamResultUpdate>();
            }
        }
        private HisVaccExamResultDelete DeleteWorker
        {
            get
            {
                return (HisVaccExamResultDelete)Worker.Get<HisVaccExamResultDelete>();
            }
        }
        private HisVaccExamResultTruncate TruncateWorker
        {
            get
            {
                return (HisVaccExamResultTruncate)Worker.Get<HisVaccExamResultTruncate>();
            }
        }
        private HisVaccExamResultGet GetWorker
        {
            get
            {
                return (HisVaccExamResultGet)Worker.Get<HisVaccExamResultGet>();
            }
        }
        private HisVaccExamResultCheck CheckWorker
        {
            get
            {
                return (HisVaccExamResultCheck)Worker.Get<HisVaccExamResultCheck>();
            }
        }

        public bool Create(HIS_VACC_EXAM_RESULT data)
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

        public bool CreateList(List<HIS_VACC_EXAM_RESULT> listData)
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

        public bool Update(HIS_VACC_EXAM_RESULT data)
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

        public bool UpdateList(List<HIS_VACC_EXAM_RESULT> listData)
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

        public bool Delete(HIS_VACC_EXAM_RESULT data)
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

        public bool DeleteList(List<HIS_VACC_EXAM_RESULT> listData)
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

        public bool Truncate(HIS_VACC_EXAM_RESULT data)
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

        public bool TruncateList(List<HIS_VACC_EXAM_RESULT> listData)
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

        public List<HIS_VACC_EXAM_RESULT> Get(HisVaccExamResultSO search, CommonParam param)
        {
            List<HIS_VACC_EXAM_RESULT> result = new List<HIS_VACC_EXAM_RESULT>();
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

        public HIS_VACC_EXAM_RESULT GetById(long id, HisVaccExamResultSO search)
        {
            HIS_VACC_EXAM_RESULT result = null;
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
