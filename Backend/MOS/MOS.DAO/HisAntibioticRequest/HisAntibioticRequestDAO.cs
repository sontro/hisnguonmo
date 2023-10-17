using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticRequest
{
    public partial class HisAntibioticRequestDAO : EntityBase
    {
        private HisAntibioticRequestCreate CreateWorker
        {
            get
            {
                return (HisAntibioticRequestCreate)Worker.Get<HisAntibioticRequestCreate>();
            }
        }
        private HisAntibioticRequestUpdate UpdateWorker
        {
            get
            {
                return (HisAntibioticRequestUpdate)Worker.Get<HisAntibioticRequestUpdate>();
            }
        }
        private HisAntibioticRequestDelete DeleteWorker
        {
            get
            {
                return (HisAntibioticRequestDelete)Worker.Get<HisAntibioticRequestDelete>();
            }
        }
        private HisAntibioticRequestTruncate TruncateWorker
        {
            get
            {
                return (HisAntibioticRequestTruncate)Worker.Get<HisAntibioticRequestTruncate>();
            }
        }
        private HisAntibioticRequestGet GetWorker
        {
            get
            {
                return (HisAntibioticRequestGet)Worker.Get<HisAntibioticRequestGet>();
            }
        }
        private HisAntibioticRequestCheck CheckWorker
        {
            get
            {
                return (HisAntibioticRequestCheck)Worker.Get<HisAntibioticRequestCheck>();
            }
        }

        public bool Create(HIS_ANTIBIOTIC_REQUEST data)
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

        public bool CreateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
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

        public bool Update(HIS_ANTIBIOTIC_REQUEST data)
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

        public bool UpdateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
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

        public bool Delete(HIS_ANTIBIOTIC_REQUEST data)
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

        public bool DeleteList(List<HIS_ANTIBIOTIC_REQUEST> listData)
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

        public bool Truncate(HIS_ANTIBIOTIC_REQUEST data)
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

        public bool TruncateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
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

        public List<HIS_ANTIBIOTIC_REQUEST> Get(HisAntibioticRequestSO search, CommonParam param)
        {
            List<HIS_ANTIBIOTIC_REQUEST> result = new List<HIS_ANTIBIOTIC_REQUEST>();
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

        public HIS_ANTIBIOTIC_REQUEST GetById(long id, HisAntibioticRequestSO search)
        {
            HIS_ANTIBIOTIC_REQUEST result = null;
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
