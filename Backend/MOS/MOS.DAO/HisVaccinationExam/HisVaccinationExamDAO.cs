using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationExam
{
    public partial class HisVaccinationExamDAO : EntityBase
    {
        private HisVaccinationExamCreate CreateWorker
        {
            get
            {
                return (HisVaccinationExamCreate)Worker.Get<HisVaccinationExamCreate>();
            }
        }
        private HisVaccinationExamUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationExamUpdate)Worker.Get<HisVaccinationExamUpdate>();
            }
        }
        private HisVaccinationExamDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationExamDelete)Worker.Get<HisVaccinationExamDelete>();
            }
        }
        private HisVaccinationExamTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationExamTruncate)Worker.Get<HisVaccinationExamTruncate>();
            }
        }
        private HisVaccinationExamGet GetWorker
        {
            get
            {
                return (HisVaccinationExamGet)Worker.Get<HisVaccinationExamGet>();
            }
        }
        private HisVaccinationExamCheck CheckWorker
        {
            get
            {
                return (HisVaccinationExamCheck)Worker.Get<HisVaccinationExamCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_EXAM data)
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

        public bool CreateList(List<HIS_VACCINATION_EXAM> listData)
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

        public bool Update(HIS_VACCINATION_EXAM data)
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

        public bool UpdateList(List<HIS_VACCINATION_EXAM> listData)
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

        public bool Delete(HIS_VACCINATION_EXAM data)
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

        public bool DeleteList(List<HIS_VACCINATION_EXAM> listData)
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

        public bool Truncate(HIS_VACCINATION_EXAM data)
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

        public bool TruncateList(List<HIS_VACCINATION_EXAM> listData)
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

        public List<HIS_VACCINATION_EXAM> Get(HisVaccinationExamSO search, CommonParam param)
        {
            List<HIS_VACCINATION_EXAM> result = new List<HIS_VACCINATION_EXAM>();
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

        public HIS_VACCINATION_EXAM GetById(long id, HisVaccinationExamSO search)
        {
            HIS_VACCINATION_EXAM result = null;
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
