using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHivTreatment
{
    public partial class HisHivTreatmentDAO : EntityBase
    {
        private HisHivTreatmentCreate CreateWorker
        {
            get
            {
                return (HisHivTreatmentCreate)Worker.Get<HisHivTreatmentCreate>();
            }
        }
        private HisHivTreatmentUpdate UpdateWorker
        {
            get
            {
                return (HisHivTreatmentUpdate)Worker.Get<HisHivTreatmentUpdate>();
            }
        }
        private HisHivTreatmentDelete DeleteWorker
        {
            get
            {
                return (HisHivTreatmentDelete)Worker.Get<HisHivTreatmentDelete>();
            }
        }
        private HisHivTreatmentTruncate TruncateWorker
        {
            get
            {
                return (HisHivTreatmentTruncate)Worker.Get<HisHivTreatmentTruncate>();
            }
        }
        private HisHivTreatmentGet GetWorker
        {
            get
            {
                return (HisHivTreatmentGet)Worker.Get<HisHivTreatmentGet>();
            }
        }
        private HisHivTreatmentCheck CheckWorker
        {
            get
            {
                return (HisHivTreatmentCheck)Worker.Get<HisHivTreatmentCheck>();
            }
        }

        public bool Create(HIS_HIV_TREATMENT data)
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

        public bool CreateList(List<HIS_HIV_TREATMENT> listData)
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

        public bool Update(HIS_HIV_TREATMENT data)
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

        public bool UpdateList(List<HIS_HIV_TREATMENT> listData)
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

        public bool Delete(HIS_HIV_TREATMENT data)
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

        public bool DeleteList(List<HIS_HIV_TREATMENT> listData)
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

        public bool Truncate(HIS_HIV_TREATMENT data)
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

        public bool TruncateList(List<HIS_HIV_TREATMENT> listData)
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

        public List<HIS_HIV_TREATMENT> Get(HisHivTreatmentSO search, CommonParam param)
        {
            List<HIS_HIV_TREATMENT> result = new List<HIS_HIV_TREATMENT>();
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

        public HIS_HIV_TREATMENT GetById(long id, HisHivTreatmentSO search)
        {
            HIS_HIV_TREATMENT result = null;
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
