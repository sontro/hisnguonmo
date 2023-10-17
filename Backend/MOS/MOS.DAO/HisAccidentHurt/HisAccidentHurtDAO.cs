using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentHurt
{
    public partial class HisAccidentHurtDAO : EntityBase
    {
        private HisAccidentHurtCreate CreateWorker
        {
            get
            {
                return (HisAccidentHurtCreate)Worker.Get<HisAccidentHurtCreate>();
            }
        }
        private HisAccidentHurtUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentHurtUpdate)Worker.Get<HisAccidentHurtUpdate>();
            }
        }
        private HisAccidentHurtDelete DeleteWorker
        {
            get
            {
                return (HisAccidentHurtDelete)Worker.Get<HisAccidentHurtDelete>();
            }
        }
        private HisAccidentHurtTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentHurtTruncate)Worker.Get<HisAccidentHurtTruncate>();
            }
        }
        private HisAccidentHurtGet GetWorker
        {
            get
            {
                return (HisAccidentHurtGet)Worker.Get<HisAccidentHurtGet>();
            }
        }
        private HisAccidentHurtCheck CheckWorker
        {
            get
            {
                return (HisAccidentHurtCheck)Worker.Get<HisAccidentHurtCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_HURT data)
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

        public bool CreateList(List<HIS_ACCIDENT_HURT> listData)
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

        public bool Update(HIS_ACCIDENT_HURT data)
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

        public bool UpdateList(List<HIS_ACCIDENT_HURT> listData)
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

        public bool Delete(HIS_ACCIDENT_HURT data)
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

        public bool DeleteList(List<HIS_ACCIDENT_HURT> listData)
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

        public bool Truncate(HIS_ACCIDENT_HURT data)
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

        public bool TruncateList(List<HIS_ACCIDENT_HURT> listData)
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

        public List<HIS_ACCIDENT_HURT> Get(HisAccidentHurtSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_HURT> result = new List<HIS_ACCIDENT_HURT>();
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

        public HIS_ACCIDENT_HURT GetById(long id, HisAccidentHurtSO search)
        {
            HIS_ACCIDENT_HURT result = null;
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
