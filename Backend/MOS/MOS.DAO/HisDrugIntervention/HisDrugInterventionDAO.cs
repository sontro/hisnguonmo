using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDrugIntervention
{
    public partial class HisDrugInterventionDAO : EntityBase
    {
        private HisDrugInterventionCreate CreateWorker
        {
            get
            {
                return (HisDrugInterventionCreate)Worker.Get<HisDrugInterventionCreate>();
            }
        }
        private HisDrugInterventionUpdate UpdateWorker
        {
            get
            {
                return (HisDrugInterventionUpdate)Worker.Get<HisDrugInterventionUpdate>();
            }
        }
        private HisDrugInterventionDelete DeleteWorker
        {
            get
            {
                return (HisDrugInterventionDelete)Worker.Get<HisDrugInterventionDelete>();
            }
        }
        private HisDrugInterventionTruncate TruncateWorker
        {
            get
            {
                return (HisDrugInterventionTruncate)Worker.Get<HisDrugInterventionTruncate>();
            }
        }
        private HisDrugInterventionGet GetWorker
        {
            get
            {
                return (HisDrugInterventionGet)Worker.Get<HisDrugInterventionGet>();
            }
        }
        private HisDrugInterventionCheck CheckWorker
        {
            get
            {
                return (HisDrugInterventionCheck)Worker.Get<HisDrugInterventionCheck>();
            }
        }

        public bool Create(HIS_DRUG_INTERVENTION data)
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

        public bool CreateList(List<HIS_DRUG_INTERVENTION> listData)
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

        public bool Update(HIS_DRUG_INTERVENTION data)
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

        public bool UpdateList(List<HIS_DRUG_INTERVENTION> listData)
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

        public bool Delete(HIS_DRUG_INTERVENTION data)
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

        public bool DeleteList(List<HIS_DRUG_INTERVENTION> listData)
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

        public bool Truncate(HIS_DRUG_INTERVENTION data)
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

        public bool TruncateList(List<HIS_DRUG_INTERVENTION> listData)
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

        public List<HIS_DRUG_INTERVENTION> Get(HisDrugInterventionSO search, CommonParam param)
        {
            List<HIS_DRUG_INTERVENTION> result = new List<HIS_DRUG_INTERVENTION>();
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

        public HIS_DRUG_INTERVENTION GetById(long id, HisDrugInterventionSO search)
        {
            HIS_DRUG_INTERVENTION result = null;
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
