using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentPoison
{
    public partial class HisAccidentPoisonDAO : EntityBase
    {
        private HisAccidentPoisonCreate CreateWorker
        {
            get
            {
                return (HisAccidentPoisonCreate)Worker.Get<HisAccidentPoisonCreate>();
            }
        }
        private HisAccidentPoisonUpdate UpdateWorker
        {
            get
            {
                return (HisAccidentPoisonUpdate)Worker.Get<HisAccidentPoisonUpdate>();
            }
        }
        private HisAccidentPoisonDelete DeleteWorker
        {
            get
            {
                return (HisAccidentPoisonDelete)Worker.Get<HisAccidentPoisonDelete>();
            }
        }
        private HisAccidentPoisonTruncate TruncateWorker
        {
            get
            {
                return (HisAccidentPoisonTruncate)Worker.Get<HisAccidentPoisonTruncate>();
            }
        }
        private HisAccidentPoisonGet GetWorker
        {
            get
            {
                return (HisAccidentPoisonGet)Worker.Get<HisAccidentPoisonGet>();
            }
        }
        private HisAccidentPoisonCheck CheckWorker
        {
            get
            {
                return (HisAccidentPoisonCheck)Worker.Get<HisAccidentPoisonCheck>();
            }
        }

        public bool Create(HIS_ACCIDENT_POISON data)
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

        public bool CreateList(List<HIS_ACCIDENT_POISON> listData)
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

        public bool Update(HIS_ACCIDENT_POISON data)
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

        public bool UpdateList(List<HIS_ACCIDENT_POISON> listData)
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

        public bool Delete(HIS_ACCIDENT_POISON data)
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

        public bool DeleteList(List<HIS_ACCIDENT_POISON> listData)
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

        public bool Truncate(HIS_ACCIDENT_POISON data)
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

        public bool TruncateList(List<HIS_ACCIDENT_POISON> listData)
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

        public List<HIS_ACCIDENT_POISON> Get(HisAccidentPoisonSO search, CommonParam param)
        {
            List<HIS_ACCIDENT_POISON> result = new List<HIS_ACCIDENT_POISON>();
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

        public HIS_ACCIDENT_POISON GetById(long id, HisAccidentPoisonSO search)
        {
            HIS_ACCIDENT_POISON result = null;
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
