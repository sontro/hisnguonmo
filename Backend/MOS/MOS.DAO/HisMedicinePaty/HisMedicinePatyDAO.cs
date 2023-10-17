using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicinePaty
{
    public partial class HisMedicinePatyDAO : EntityBase
    {
        private HisMedicinePatyCreate CreateWorker
        {
            get
            {
                return (HisMedicinePatyCreate)Worker.Get<HisMedicinePatyCreate>();
            }
        }
        private HisMedicinePatyUpdate UpdateWorker
        {
            get
            {
                return (HisMedicinePatyUpdate)Worker.Get<HisMedicinePatyUpdate>();
            }
        }
        private HisMedicinePatyDelete DeleteWorker
        {
            get
            {
                return (HisMedicinePatyDelete)Worker.Get<HisMedicinePatyDelete>();
            }
        }
        private HisMedicinePatyTruncate TruncateWorker
        {
            get
            {
                return (HisMedicinePatyTruncate)Worker.Get<HisMedicinePatyTruncate>();
            }
        }
        private HisMedicinePatyGet GetWorker
        {
            get
            {
                return (HisMedicinePatyGet)Worker.Get<HisMedicinePatyGet>();
            }
        }
        private HisMedicinePatyCheck CheckWorker
        {
            get
            {
                return (HisMedicinePatyCheck)Worker.Get<HisMedicinePatyCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_PATY data)
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

        public bool CreateList(List<HIS_MEDICINE_PATY> listData)
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

        public bool Update(HIS_MEDICINE_PATY data)
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

        public bool UpdateList(List<HIS_MEDICINE_PATY> listData)
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

        public bool Delete(HIS_MEDICINE_PATY data)
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

        public bool DeleteList(List<HIS_MEDICINE_PATY> listData)
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

        public bool Truncate(HIS_MEDICINE_PATY data)
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

        public bool TruncateList(List<HIS_MEDICINE_PATY> listData)
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

        public List<HIS_MEDICINE_PATY> Get(HisMedicinePatySO search, CommonParam param)
        {
            List<HIS_MEDICINE_PATY> result = new List<HIS_MEDICINE_PATY>();
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

        public HIS_MEDICINE_PATY GetById(long id, HisMedicinePatySO search)
        {
            HIS_MEDICINE_PATY result = null;
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
