using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccineType
{
    public partial class HisVaccineTypeDAO : EntityBase
    {
        private HisVaccineTypeCreate CreateWorker
        {
            get
            {
                return (HisVaccineTypeCreate)Worker.Get<HisVaccineTypeCreate>();
            }
        }
        private HisVaccineTypeUpdate UpdateWorker
        {
            get
            {
                return (HisVaccineTypeUpdate)Worker.Get<HisVaccineTypeUpdate>();
            }
        }
        private HisVaccineTypeDelete DeleteWorker
        {
            get
            {
                return (HisVaccineTypeDelete)Worker.Get<HisVaccineTypeDelete>();
            }
        }
        private HisVaccineTypeTruncate TruncateWorker
        {
            get
            {
                return (HisVaccineTypeTruncate)Worker.Get<HisVaccineTypeTruncate>();
            }
        }
        private HisVaccineTypeGet GetWorker
        {
            get
            {
                return (HisVaccineTypeGet)Worker.Get<HisVaccineTypeGet>();
            }
        }
        private HisVaccineTypeCheck CheckWorker
        {
            get
            {
                return (HisVaccineTypeCheck)Worker.Get<HisVaccineTypeCheck>();
            }
        }

        public bool Create(HIS_VACCINE_TYPE data)
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

        public bool CreateList(List<HIS_VACCINE_TYPE> listData)
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

        public bool Update(HIS_VACCINE_TYPE data)
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

        public bool UpdateList(List<HIS_VACCINE_TYPE> listData)
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

        public bool Delete(HIS_VACCINE_TYPE data)
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

        public bool DeleteList(List<HIS_VACCINE_TYPE> listData)
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

        public bool Truncate(HIS_VACCINE_TYPE data)
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

        public bool TruncateList(List<HIS_VACCINE_TYPE> listData)
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

        public List<HIS_VACCINE_TYPE> Get(HisVaccineTypeSO search, CommonParam param)
        {
            List<HIS_VACCINE_TYPE> result = new List<HIS_VACCINE_TYPE>();
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

        public HIS_VACCINE_TYPE GetById(long id, HisVaccineTypeSO search)
        {
            HIS_VACCINE_TYPE result = null;
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
