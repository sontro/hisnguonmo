using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrpl
{
    public partial class HisVaccinationVrplDAO : EntityBase
    {
        private HisVaccinationVrplCreate CreateWorker
        {
            get
            {
                return (HisVaccinationVrplCreate)Worker.Get<HisVaccinationVrplCreate>();
            }
        }
        private HisVaccinationVrplUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationVrplUpdate)Worker.Get<HisVaccinationVrplUpdate>();
            }
        }
        private HisVaccinationVrplDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationVrplDelete)Worker.Get<HisVaccinationVrplDelete>();
            }
        }
        private HisVaccinationVrplTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationVrplTruncate)Worker.Get<HisVaccinationVrplTruncate>();
            }
        }
        private HisVaccinationVrplGet GetWorker
        {
            get
            {
                return (HisVaccinationVrplGet)Worker.Get<HisVaccinationVrplGet>();
            }
        }
        private HisVaccinationVrplCheck CheckWorker
        {
            get
            {
                return (HisVaccinationVrplCheck)Worker.Get<HisVaccinationVrplCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_VRPL data)
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

        public bool CreateList(List<HIS_VACCINATION_VRPL> listData)
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

        public bool Update(HIS_VACCINATION_VRPL data)
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

        public bool UpdateList(List<HIS_VACCINATION_VRPL> listData)
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

        public bool Delete(HIS_VACCINATION_VRPL data)
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

        public bool DeleteList(List<HIS_VACCINATION_VRPL> listData)
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

        public bool Truncate(HIS_VACCINATION_VRPL data)
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

        public bool TruncateList(List<HIS_VACCINATION_VRPL> listData)
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

        public List<HIS_VACCINATION_VRPL> Get(HisVaccinationVrplSO search, CommonParam param)
        {
            List<HIS_VACCINATION_VRPL> result = new List<HIS_VACCINATION_VRPL>();
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

        public HIS_VACCINATION_VRPL GetById(long id, HisVaccinationVrplSO search)
        {
            HIS_VACCINATION_VRPL result = null;
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
