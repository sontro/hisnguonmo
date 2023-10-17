using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestMedicine
{
    public partial class HisImpMestMedicineDAO : EntityBase
    {
        private HisImpMestMedicineCreate CreateWorker
        {
            get
            {
                return (HisImpMestMedicineCreate)Worker.Get<HisImpMestMedicineCreate>();
            }
        }
        private HisImpMestMedicineUpdate UpdateWorker
        {
            get
            {
                return (HisImpMestMedicineUpdate)Worker.Get<HisImpMestMedicineUpdate>();
            }
        }
        private HisImpMestMedicineDelete DeleteWorker
        {
            get
            {
                return (HisImpMestMedicineDelete)Worker.Get<HisImpMestMedicineDelete>();
            }
        }
        private HisImpMestMedicineTruncate TruncateWorker
        {
            get
            {
                return (HisImpMestMedicineTruncate)Worker.Get<HisImpMestMedicineTruncate>();
            }
        }
        private HisImpMestMedicineGet GetWorker
        {
            get
            {
                return (HisImpMestMedicineGet)Worker.Get<HisImpMestMedicineGet>();
            }
        }
        private HisImpMestMedicineCheck CheckWorker
        {
            get
            {
                return (HisImpMestMedicineCheck)Worker.Get<HisImpMestMedicineCheck>();
            }
        }

        public bool Create(HIS_IMP_MEST_MEDICINE data)
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

        public bool CreateList(List<HIS_IMP_MEST_MEDICINE> listData)
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

        public bool Update(HIS_IMP_MEST_MEDICINE data)
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

        public bool UpdateList(List<HIS_IMP_MEST_MEDICINE> listData)
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

        public bool Delete(HIS_IMP_MEST_MEDICINE data)
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

        public bool DeleteList(List<HIS_IMP_MEST_MEDICINE> listData)
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

        public bool Truncate(HIS_IMP_MEST_MEDICINE data)
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

        public bool TruncateList(List<HIS_IMP_MEST_MEDICINE> listData)
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

        public List<HIS_IMP_MEST_MEDICINE> Get(HisImpMestMedicineSO search, CommonParam param)
        {
            List<HIS_IMP_MEST_MEDICINE> result = new List<HIS_IMP_MEST_MEDICINE>();
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

        public HIS_IMP_MEST_MEDICINE GetById(long id, HisImpMestMedicineSO search)
        {
            HIS_IMP_MEST_MEDICINE result = null;
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
