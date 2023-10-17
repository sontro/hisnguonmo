using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisBidBloodType
{
    public partial class HisBidBloodTypeDAO : EntityBase
    {
        private HisBidBloodTypeCreate CreateWorker
        {
            get
            {
                return (HisBidBloodTypeCreate)Worker.Get<HisBidBloodTypeCreate>();
            }
        }
        private HisBidBloodTypeUpdate UpdateWorker
        {
            get
            {
                return (HisBidBloodTypeUpdate)Worker.Get<HisBidBloodTypeUpdate>();
            }
        }
        private HisBidBloodTypeDelete DeleteWorker
        {
            get
            {
                return (HisBidBloodTypeDelete)Worker.Get<HisBidBloodTypeDelete>();
            }
        }
        private HisBidBloodTypeTruncate TruncateWorker
        {
            get
            {
                return (HisBidBloodTypeTruncate)Worker.Get<HisBidBloodTypeTruncate>();
            }
        }
        private HisBidBloodTypeGet GetWorker
        {
            get
            {
                return (HisBidBloodTypeGet)Worker.Get<HisBidBloodTypeGet>();
            }
        }
        private HisBidBloodTypeCheck CheckWorker
        {
            get
            {
                return (HisBidBloodTypeCheck)Worker.Get<HisBidBloodTypeCheck>();
            }
        }

        public bool Create(HIS_BID_BLOOD_TYPE data)
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

        public bool CreateList(List<HIS_BID_BLOOD_TYPE> listData)
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

        public bool Update(HIS_BID_BLOOD_TYPE data)
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

        public bool UpdateList(List<HIS_BID_BLOOD_TYPE> listData)
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

        public bool Delete(HIS_BID_BLOOD_TYPE data)
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

        public bool DeleteList(List<HIS_BID_BLOOD_TYPE> listData)
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

        public bool Truncate(HIS_BID_BLOOD_TYPE data)
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

        public bool TruncateList(List<HIS_BID_BLOOD_TYPE> listData)
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

        public List<HIS_BID_BLOOD_TYPE> Get(HisBidBloodTypeSO search, CommonParam param)
        {
            List<HIS_BID_BLOOD_TYPE> result = new List<HIS_BID_BLOOD_TYPE>();
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

        public HIS_BID_BLOOD_TYPE GetById(long id, HisBidBloodTypeSO search)
        {
            HIS_BID_BLOOD_TYPE result = null;
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
