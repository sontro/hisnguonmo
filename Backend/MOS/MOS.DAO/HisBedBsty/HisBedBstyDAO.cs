using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBedBsty
{
    public partial class HisBedBstyDAO : EntityBase
    {
        private HisBedBstyCreate CreateWorker
        {
            get
            {
                return (HisBedBstyCreate)Worker.Get<HisBedBstyCreate>();
            }
        }
        private HisBedBstyUpdate UpdateWorker
        {
            get
            {
                return (HisBedBstyUpdate)Worker.Get<HisBedBstyUpdate>();
            }
        }
        private HisBedBstyDelete DeleteWorker
        {
            get
            {
                return (HisBedBstyDelete)Worker.Get<HisBedBstyDelete>();
            }
        }
        private HisBedBstyTruncate TruncateWorker
        {
            get
            {
                return (HisBedBstyTruncate)Worker.Get<HisBedBstyTruncate>();
            }
        }
        private HisBedBstyGet GetWorker
        {
            get
            {
                return (HisBedBstyGet)Worker.Get<HisBedBstyGet>();
            }
        }
        private HisBedBstyCheck CheckWorker
        {
            get
            {
                return (HisBedBstyCheck)Worker.Get<HisBedBstyCheck>();
            }
        }

        public bool Create(HIS_BED_BSTY data)
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

        public bool CreateList(List<HIS_BED_BSTY> listData)
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

        public bool Update(HIS_BED_BSTY data)
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

        public bool UpdateList(List<HIS_BED_BSTY> listData)
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

        public bool Delete(HIS_BED_BSTY data)
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

        public bool DeleteList(List<HIS_BED_BSTY> listData)
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

        public bool Truncate(HIS_BED_BSTY data)
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

        public bool TruncateList(List<HIS_BED_BSTY> listData)
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

        public List<HIS_BED_BSTY> Get(HisBedBstySO search, CommonParam param)
        {
            List<HIS_BED_BSTY> result = new List<HIS_BED_BSTY>();
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

        public HIS_BED_BSTY GetById(long id, HisBedBstySO search)
        {
            HIS_BED_BSTY result = null;
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
