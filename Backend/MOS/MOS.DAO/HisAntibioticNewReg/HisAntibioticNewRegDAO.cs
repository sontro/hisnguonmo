using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticNewReg
{
    public partial class HisAntibioticNewRegDAO : EntityBase
    {
        private HisAntibioticNewRegCreate CreateWorker
        {
            get
            {
                return (HisAntibioticNewRegCreate)Worker.Get<HisAntibioticNewRegCreate>();
            }
        }
        private HisAntibioticNewRegUpdate UpdateWorker
        {
            get
            {
                return (HisAntibioticNewRegUpdate)Worker.Get<HisAntibioticNewRegUpdate>();
            }
        }
        private HisAntibioticNewRegDelete DeleteWorker
        {
            get
            {
                return (HisAntibioticNewRegDelete)Worker.Get<HisAntibioticNewRegDelete>();
            }
        }
        private HisAntibioticNewRegTruncate TruncateWorker
        {
            get
            {
                return (HisAntibioticNewRegTruncate)Worker.Get<HisAntibioticNewRegTruncate>();
            }
        }
        private HisAntibioticNewRegGet GetWorker
        {
            get
            {
                return (HisAntibioticNewRegGet)Worker.Get<HisAntibioticNewRegGet>();
            }
        }
        private HisAntibioticNewRegCheck CheckWorker
        {
            get
            {
                return (HisAntibioticNewRegCheck)Worker.Get<HisAntibioticNewRegCheck>();
            }
        }

        public bool Create(HIS_ANTIBIOTIC_NEW_REG data)
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

        public bool CreateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
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

        public bool Update(HIS_ANTIBIOTIC_NEW_REG data)
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

        public bool UpdateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
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

        public bool Delete(HIS_ANTIBIOTIC_NEW_REG data)
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

        public bool DeleteList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
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

        public bool Truncate(HIS_ANTIBIOTIC_NEW_REG data)
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

        public bool TruncateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
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

        public List<HIS_ANTIBIOTIC_NEW_REG> Get(HisAntibioticNewRegSO search, CommonParam param)
        {
            List<HIS_ANTIBIOTIC_NEW_REG> result = new List<HIS_ANTIBIOTIC_NEW_REG>();
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

        public HIS_ANTIBIOTIC_NEW_REG GetById(long id, HisAntibioticNewRegSO search)
        {
            HIS_ANTIBIOTIC_NEW_REG result = null;
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
