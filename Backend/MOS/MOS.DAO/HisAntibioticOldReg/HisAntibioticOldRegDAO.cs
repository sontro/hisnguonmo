using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticOldReg
{
    public partial class HisAntibioticOldRegDAO : EntityBase
    {
        private HisAntibioticOldRegCreate CreateWorker
        {
            get
            {
                return (HisAntibioticOldRegCreate)Worker.Get<HisAntibioticOldRegCreate>();
            }
        }
        private HisAntibioticOldRegUpdate UpdateWorker
        {
            get
            {
                return (HisAntibioticOldRegUpdate)Worker.Get<HisAntibioticOldRegUpdate>();
            }
        }
        private HisAntibioticOldRegDelete DeleteWorker
        {
            get
            {
                return (HisAntibioticOldRegDelete)Worker.Get<HisAntibioticOldRegDelete>();
            }
        }
        private HisAntibioticOldRegTruncate TruncateWorker
        {
            get
            {
                return (HisAntibioticOldRegTruncate)Worker.Get<HisAntibioticOldRegTruncate>();
            }
        }
        private HisAntibioticOldRegGet GetWorker
        {
            get
            {
                return (HisAntibioticOldRegGet)Worker.Get<HisAntibioticOldRegGet>();
            }
        }
        private HisAntibioticOldRegCheck CheckWorker
        {
            get
            {
                return (HisAntibioticOldRegCheck)Worker.Get<HisAntibioticOldRegCheck>();
            }
        }

        public bool Create(HIS_ANTIBIOTIC_OLD_REG data)
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

        public bool CreateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
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

        public bool Update(HIS_ANTIBIOTIC_OLD_REG data)
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

        public bool UpdateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
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

        public bool Delete(HIS_ANTIBIOTIC_OLD_REG data)
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

        public bool DeleteList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
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

        public bool Truncate(HIS_ANTIBIOTIC_OLD_REG data)
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

        public bool TruncateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
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

        public List<HIS_ANTIBIOTIC_OLD_REG> Get(HisAntibioticOldRegSO search, CommonParam param)
        {
            List<HIS_ANTIBIOTIC_OLD_REG> result = new List<HIS_ANTIBIOTIC_OLD_REG>();
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

        public HIS_ANTIBIOTIC_OLD_REG GetById(long id, HisAntibioticOldRegSO search)
        {
            HIS_ANTIBIOTIC_OLD_REG result = null;
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
