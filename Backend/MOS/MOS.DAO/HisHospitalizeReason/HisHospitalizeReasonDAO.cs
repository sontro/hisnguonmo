using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisHospitalizeReason
{
    public partial class HisHospitalizeReasonDAO : EntityBase
    {
        private HisHospitalizeReasonCreate CreateWorker
        {
            get
            {
                return (HisHospitalizeReasonCreate)Worker.Get<HisHospitalizeReasonCreate>();
            }
        }
        private HisHospitalizeReasonUpdate UpdateWorker
        {
            get
            {
                return (HisHospitalizeReasonUpdate)Worker.Get<HisHospitalizeReasonUpdate>();
            }
        }
        private HisHospitalizeReasonDelete DeleteWorker
        {
            get
            {
                return (HisHospitalizeReasonDelete)Worker.Get<HisHospitalizeReasonDelete>();
            }
        }
        private HisHospitalizeReasonTruncate TruncateWorker
        {
            get
            {
                return (HisHospitalizeReasonTruncate)Worker.Get<HisHospitalizeReasonTruncate>();
            }
        }
        private HisHospitalizeReasonGet GetWorker
        {
            get
            {
                return (HisHospitalizeReasonGet)Worker.Get<HisHospitalizeReasonGet>();
            }
        }
        private HisHospitalizeReasonCheck CheckWorker
        {
            get
            {
                return (HisHospitalizeReasonCheck)Worker.Get<HisHospitalizeReasonCheck>();
            }
        }

        public bool Create(HIS_HOSPITALIZE_REASON data)
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

        public bool CreateList(List<HIS_HOSPITALIZE_REASON> listData)
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

        public bool Update(HIS_HOSPITALIZE_REASON data)
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

        public bool UpdateList(List<HIS_HOSPITALIZE_REASON> listData)
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

        public bool Delete(HIS_HOSPITALIZE_REASON data)
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

        public bool DeleteList(List<HIS_HOSPITALIZE_REASON> listData)
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

        public bool Truncate(HIS_HOSPITALIZE_REASON data)
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

        public bool TruncateList(List<HIS_HOSPITALIZE_REASON> listData)
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

        public List<HIS_HOSPITALIZE_REASON> Get(HisHospitalizeReasonSO search, CommonParam param)
        {
            List<HIS_HOSPITALIZE_REASON> result = new List<HIS_HOSPITALIZE_REASON>();
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

        public HIS_HOSPITALIZE_REASON GetById(long id, HisHospitalizeReasonSO search)
        {
            HIS_HOSPITALIZE_REASON result = null;
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
