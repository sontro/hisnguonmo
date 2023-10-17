using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBloodRh
{
    public partial class HisBloodRhDAO : EntityBase
    {
        private HisBloodRhCreate CreateWorker
        {
            get
            {
                return (HisBloodRhCreate)Worker.Get<HisBloodRhCreate>();
            }
        }
        private HisBloodRhUpdate UpdateWorker
        {
            get
            {
                return (HisBloodRhUpdate)Worker.Get<HisBloodRhUpdate>();
            }
        }
        private HisBloodRhDelete DeleteWorker
        {
            get
            {
                return (HisBloodRhDelete)Worker.Get<HisBloodRhDelete>();
            }
        }
        private HisBloodRhTruncate TruncateWorker
        {
            get
            {
                return (HisBloodRhTruncate)Worker.Get<HisBloodRhTruncate>();
            }
        }
        private HisBloodRhGet GetWorker
        {
            get
            {
                return (HisBloodRhGet)Worker.Get<HisBloodRhGet>();
            }
        }
        private HisBloodRhCheck CheckWorker
        {
            get
            {
                return (HisBloodRhCheck)Worker.Get<HisBloodRhCheck>();
            }
        }

        public bool Create(HIS_BLOOD_RH data)
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

        public bool CreateList(List<HIS_BLOOD_RH> listData)
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

        public bool Update(HIS_BLOOD_RH data)
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

        public bool UpdateList(List<HIS_BLOOD_RH> listData)
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

        public bool Delete(HIS_BLOOD_RH data)
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

        public bool DeleteList(List<HIS_BLOOD_RH> listData)
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

        public bool Truncate(HIS_BLOOD_RH data)
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

        public bool TruncateList(List<HIS_BLOOD_RH> listData)
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

        public List<HIS_BLOOD_RH> Get(HisBloodRhSO search, CommonParam param)
        {
            List<HIS_BLOOD_RH> result = new List<HIS_BLOOD_RH>();
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

        public HIS_BLOOD_RH GetById(long id, HisBloodRhSO search)
        {
            HIS_BLOOD_RH result = null;
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
