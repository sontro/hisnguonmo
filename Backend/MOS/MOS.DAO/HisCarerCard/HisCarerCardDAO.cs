using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisCarerCard
{
    public partial class HisCarerCardDAO : EntityBase
    {
        private HisCarerCardCreate CreateWorker
        {
            get
            {
                return (HisCarerCardCreate)Worker.Get<HisCarerCardCreate>();
            }
        }
        private HisCarerCardUpdate UpdateWorker
        {
            get
            {
                return (HisCarerCardUpdate)Worker.Get<HisCarerCardUpdate>();
            }
        }
        private HisCarerCardDelete DeleteWorker
        {
            get
            {
                return (HisCarerCardDelete)Worker.Get<HisCarerCardDelete>();
            }
        }
        private HisCarerCardTruncate TruncateWorker
        {
            get
            {
                return (HisCarerCardTruncate)Worker.Get<HisCarerCardTruncate>();
            }
        }
        private HisCarerCardGet GetWorker
        {
            get
            {
                return (HisCarerCardGet)Worker.Get<HisCarerCardGet>();
            }
        }
        private HisCarerCardCheck CheckWorker
        {
            get
            {
                return (HisCarerCardCheck)Worker.Get<HisCarerCardCheck>();
            }
        }

        public bool Create(HIS_CARER_CARD data)
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

        public bool CreateList(List<HIS_CARER_CARD> listData)
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

        public bool Update(HIS_CARER_CARD data)
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

        public bool UpdateList(List<HIS_CARER_CARD> listData)
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

        public bool Delete(HIS_CARER_CARD data)
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

        public bool DeleteList(List<HIS_CARER_CARD> listData)
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

        public bool Truncate(HIS_CARER_CARD data)
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

        public bool TruncateList(List<HIS_CARER_CARD> listData)
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

        public List<HIS_CARER_CARD> Get(HisCarerCardSO search, CommonParam param)
        {
            List<HIS_CARER_CARD> result = new List<HIS_CARER_CARD>();
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

        public HIS_CARER_CARD GetById(long id, HisCarerCardSO search)
        {
            HIS_CARER_CARD result = null;
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
