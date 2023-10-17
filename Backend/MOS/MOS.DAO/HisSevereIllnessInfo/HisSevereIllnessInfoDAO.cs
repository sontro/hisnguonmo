using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoDAO : EntityBase
    {
        private HisSevereIllnessInfoCreate CreateWorker
        {
            get
            {
                return (HisSevereIllnessInfoCreate)Worker.Get<HisSevereIllnessInfoCreate>();
            }
        }
        private HisSevereIllnessInfoUpdate UpdateWorker
        {
            get
            {
                return (HisSevereIllnessInfoUpdate)Worker.Get<HisSevereIllnessInfoUpdate>();
            }
        }
        private HisSevereIllnessInfoDelete DeleteWorker
        {
            get
            {
                return (HisSevereIllnessInfoDelete)Worker.Get<HisSevereIllnessInfoDelete>();
            }
        }
        private HisSevereIllnessInfoTruncate TruncateWorker
        {
            get
            {
                return (HisSevereIllnessInfoTruncate)Worker.Get<HisSevereIllnessInfoTruncate>();
            }
        }
        private HisSevereIllnessInfoGet GetWorker
        {
            get
            {
                return (HisSevereIllnessInfoGet)Worker.Get<HisSevereIllnessInfoGet>();
            }
        }
        private HisSevereIllnessInfoCheck CheckWorker
        {
            get
            {
                return (HisSevereIllnessInfoCheck)Worker.Get<HisSevereIllnessInfoCheck>();
            }
        }

        public bool Create(HIS_SEVERE_ILLNESS_INFO data)
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

        public bool CreateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
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

        public bool Update(HIS_SEVERE_ILLNESS_INFO data)
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

        public bool UpdateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
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

        public bool Delete(HIS_SEVERE_ILLNESS_INFO data)
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

        public bool DeleteList(List<HIS_SEVERE_ILLNESS_INFO> listData)
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

        public bool Truncate(HIS_SEVERE_ILLNESS_INFO data)
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

        public bool TruncateList(List<HIS_SEVERE_ILLNESS_INFO> listData)
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

        public List<HIS_SEVERE_ILLNESS_INFO> Get(HisSevereIllnessInfoSO search, CommonParam param)
        {
            List<HIS_SEVERE_ILLNESS_INFO> result = new List<HIS_SEVERE_ILLNESS_INFO>();
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

        public HIS_SEVERE_ILLNESS_INFO GetById(long id, HisSevereIllnessInfoSO search)
        {
            HIS_SEVERE_ILLNESS_INFO result = null;
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
