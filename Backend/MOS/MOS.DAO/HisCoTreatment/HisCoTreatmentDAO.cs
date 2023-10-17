using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCoTreatment
{
    public partial class HisCoTreatmentDAO : EntityBase
    {
        private HisCoTreatmentCreate CreateWorker
        {
            get
            {
                return (HisCoTreatmentCreate)Worker.Get<HisCoTreatmentCreate>();
            }
        }
        private HisCoTreatmentUpdate UpdateWorker
        {
            get
            {
                return (HisCoTreatmentUpdate)Worker.Get<HisCoTreatmentUpdate>();
            }
        }
        private HisCoTreatmentDelete DeleteWorker
        {
            get
            {
                return (HisCoTreatmentDelete)Worker.Get<HisCoTreatmentDelete>();
            }
        }
        private HisCoTreatmentTruncate TruncateWorker
        {
            get
            {
                return (HisCoTreatmentTruncate)Worker.Get<HisCoTreatmentTruncate>();
            }
        }
        private HisCoTreatmentGet GetWorker
        {
            get
            {
                return (HisCoTreatmentGet)Worker.Get<HisCoTreatmentGet>();
            }
        }
        private HisCoTreatmentCheck CheckWorker
        {
            get
            {
                return (HisCoTreatmentCheck)Worker.Get<HisCoTreatmentCheck>();
            }
        }

        public bool Create(HIS_CO_TREATMENT data)
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

        public bool CreateList(List<HIS_CO_TREATMENT> listData)
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

        public bool Update(HIS_CO_TREATMENT data)
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

        public bool UpdateList(List<HIS_CO_TREATMENT> listData)
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

        public bool Delete(HIS_CO_TREATMENT data)
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

        public bool DeleteList(List<HIS_CO_TREATMENT> listData)
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

        public bool Truncate(HIS_CO_TREATMENT data)
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

        public bool TruncateList(List<HIS_CO_TREATMENT> listData)
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

        public List<HIS_CO_TREATMENT> Get(HisCoTreatmentSO search, CommonParam param)
        {
            List<HIS_CO_TREATMENT> result = new List<HIS_CO_TREATMENT>();
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

        public HIS_CO_TREATMENT GetById(long id, HisCoTreatmentSO search)
        {
            HIS_CO_TREATMENT result = null;
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
