using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineInteractive
{
    public partial class HisMedicineInteractiveDAO : EntityBase
    {
        private HisMedicineInteractiveCreate CreateWorker
        {
            get
            {
                return (HisMedicineInteractiveCreate)Worker.Get<HisMedicineInteractiveCreate>();
            }
        }
        private HisMedicineInteractiveUpdate UpdateWorker
        {
            get
            {
                return (HisMedicineInteractiveUpdate)Worker.Get<HisMedicineInteractiveUpdate>();
            }
        }
        private HisMedicineInteractiveDelete DeleteWorker
        {
            get
            {
                return (HisMedicineInteractiveDelete)Worker.Get<HisMedicineInteractiveDelete>();
            }
        }
        private HisMedicineInteractiveTruncate TruncateWorker
        {
            get
            {
                return (HisMedicineInteractiveTruncate)Worker.Get<HisMedicineInteractiveTruncate>();
            }
        }
        private HisMedicineInteractiveGet GetWorker
        {
            get
            {
                return (HisMedicineInteractiveGet)Worker.Get<HisMedicineInteractiveGet>();
            }
        }
        private HisMedicineInteractiveCheck CheckWorker
        {
            get
            {
                return (HisMedicineInteractiveCheck)Worker.Get<HisMedicineInteractiveCheck>();
            }
        }

        public bool Create(HIS_MEDICINE_INTERACTIVE data)
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

        public bool CreateList(List<HIS_MEDICINE_INTERACTIVE> listData)
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

        public bool Update(HIS_MEDICINE_INTERACTIVE data)
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

        public bool UpdateList(List<HIS_MEDICINE_INTERACTIVE> listData)
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

        public bool Delete(HIS_MEDICINE_INTERACTIVE data)
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

        public bool DeleteList(List<HIS_MEDICINE_INTERACTIVE> listData)
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

        public bool Truncate(HIS_MEDICINE_INTERACTIVE data)
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

        public bool TruncateList(List<HIS_MEDICINE_INTERACTIVE> listData)
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

        public List<HIS_MEDICINE_INTERACTIVE> Get(HisMedicineInteractiveSO search, CommonParam param)
        {
            List<HIS_MEDICINE_INTERACTIVE> result = new List<HIS_MEDICINE_INTERACTIVE>();
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

        public HIS_MEDICINE_INTERACTIVE GetById(long id, HisMedicineInteractiveSO search)
        {
            HIS_MEDICINE_INTERACTIVE result = null;
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
