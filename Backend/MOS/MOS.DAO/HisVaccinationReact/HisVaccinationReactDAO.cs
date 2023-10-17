using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationReact
{
    public partial class HisVaccinationReactDAO : EntityBase
    {
        private HisVaccinationReactCreate CreateWorker
        {
            get
            {
                return (HisVaccinationReactCreate)Worker.Get<HisVaccinationReactCreate>();
            }
        }
        private HisVaccinationReactUpdate UpdateWorker
        {
            get
            {
                return (HisVaccinationReactUpdate)Worker.Get<HisVaccinationReactUpdate>();
            }
        }
        private HisVaccinationReactDelete DeleteWorker
        {
            get
            {
                return (HisVaccinationReactDelete)Worker.Get<HisVaccinationReactDelete>();
            }
        }
        private HisVaccinationReactTruncate TruncateWorker
        {
            get
            {
                return (HisVaccinationReactTruncate)Worker.Get<HisVaccinationReactTruncate>();
            }
        }
        private HisVaccinationReactGet GetWorker
        {
            get
            {
                return (HisVaccinationReactGet)Worker.Get<HisVaccinationReactGet>();
            }
        }
        private HisVaccinationReactCheck CheckWorker
        {
            get
            {
                return (HisVaccinationReactCheck)Worker.Get<HisVaccinationReactCheck>();
            }
        }

        public bool Create(HIS_VACCINATION_REACT data)
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

        public bool CreateList(List<HIS_VACCINATION_REACT> listData)
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

        public bool Update(HIS_VACCINATION_REACT data)
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

        public bool UpdateList(List<HIS_VACCINATION_REACT> listData)
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

        public bool Delete(HIS_VACCINATION_REACT data)
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

        public bool DeleteList(List<HIS_VACCINATION_REACT> listData)
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

        public bool Truncate(HIS_VACCINATION_REACT data)
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

        public bool TruncateList(List<HIS_VACCINATION_REACT> listData)
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

        public List<HIS_VACCINATION_REACT> Get(HisVaccinationReactSO search, CommonParam param)
        {
            List<HIS_VACCINATION_REACT> result = new List<HIS_VACCINATION_REACT>();
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

        public HIS_VACCINATION_REACT GetById(long id, HisVaccinationReactSO search)
        {
            HIS_VACCINATION_REACT result = null;
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
