using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrty
{
    public partial class HisVaccinationVrtyDAO : EntityBase
    {
        private HisVaccinationVrtyCreate CreateWorker
        {
            get
            {
                return (HisVaccinationVrtyCreate)Worker.Get<HisVaccinationVrtyCreate>();
            }
        }
        private HisVaccinationVrtyUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationVrtyUpdate)Worker.Get<HisVaccinationVrtyUpdate>();
            }
        }
        private HisVaccinationVrtyDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationVrtyDelete)Worker.Get<HisVaccinationVrtyDelete>();
            }
        }
        private HisVaccinationVrtyTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationVrtyTruncate)Worker.Get<HisVaccinationVrtyTruncate>();
            }
        }
        private HisVaccinationVrtyGet GetWorker
        {
            get
            {
                return (HisVaccinationVrtyGet)Worker.Get<HisVaccinationVrtyGet>();
            }
        }
        private HisVaccinationVrtyCheck CheckWorker
        {
            get
            {
                return (HisVaccinationVrtyCheck)Worker.Get<HisVaccinationVrtyCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_VRTY data)
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

        public bool CreateList(List<HIS_VACCINATION_VRTY> listData)
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

        public bool Update(HIS_VACCINATION_VRTY data)
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

        public bool UpdateList(List<HIS_VACCINATION_VRTY> listData)
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

        public bool Delete(HIS_VACCINATION_VRTY data)
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

        public bool DeleteList(List<HIS_VACCINATION_VRTY> listData)
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

        public bool Truncate(HIS_VACCINATION_VRTY data)
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

        public bool TruncateList(List<HIS_VACCINATION_VRTY> listData)
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

        public List<HIS_VACCINATION_VRTY> Get(HisVaccinationVrtySO search, CommonParam param)
        {
            List<HIS_VACCINATION_VRTY> result = new List<HIS_VACCINATION_VRTY>();
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

        public HIS_VACCINATION_VRTY GetById(long id, HisVaccinationVrtySO search)
        {
            HIS_VACCINATION_VRTY result = null;
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
