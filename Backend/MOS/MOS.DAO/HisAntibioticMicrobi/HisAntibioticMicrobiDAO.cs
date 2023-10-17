using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticMicrobi
{
    public partial class HisAntibioticMicrobiDAO : EntityBase
    {
        private HisAntibioticMicrobiCreate CreateWorker
        {
            get
            {
                return (HisAntibioticMicrobiCreate)Worker.Get<HisAntibioticMicrobiCreate>();
            }
        }
        private HisAntibioticMicrobiUpdate UpdateWorker
        {
            get
            {
                return (HisAntibioticMicrobiUpdate)Worker.Get<HisAntibioticMicrobiUpdate>();
            }
        }
        private HisAntibioticMicrobiDelete DeleteWorker
        {
            get
            {
                return (HisAntibioticMicrobiDelete)Worker.Get<HisAntibioticMicrobiDelete>();
            }
        }
        private HisAntibioticMicrobiTruncate TruncateWorker
        {
            get
            {
                return (HisAntibioticMicrobiTruncate)Worker.Get<HisAntibioticMicrobiTruncate>();
            }
        }
        private HisAntibioticMicrobiGet GetWorker
        {
            get
            {
                return (HisAntibioticMicrobiGet)Worker.Get<HisAntibioticMicrobiGet>();
            }
        }
        private HisAntibioticMicrobiCheck CheckWorker
        {
            get
            {
                return (HisAntibioticMicrobiCheck)Worker.Get<HisAntibioticMicrobiCheck>();
            }
        }

        public bool Create(HIS_ANTIBIOTIC_MICROBI data)
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

        public bool CreateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
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

        public bool Update(HIS_ANTIBIOTIC_MICROBI data)
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

        public bool UpdateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
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

        public bool Delete(HIS_ANTIBIOTIC_MICROBI data)
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

        public bool DeleteList(List<HIS_ANTIBIOTIC_MICROBI> listData)
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

        public bool Truncate(HIS_ANTIBIOTIC_MICROBI data)
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

        public bool TruncateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
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

        public List<HIS_ANTIBIOTIC_MICROBI> Get(HisAntibioticMicrobiSO search, CommonParam param)
        {
            List<HIS_ANTIBIOTIC_MICROBI> result = new List<HIS_ANTIBIOTIC_MICROBI>();
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

        public HIS_ANTIBIOTIC_MICROBI GetById(long id, HisAntibioticMicrobiSO search)
        {
            HIS_ANTIBIOTIC_MICROBI result = null;
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
