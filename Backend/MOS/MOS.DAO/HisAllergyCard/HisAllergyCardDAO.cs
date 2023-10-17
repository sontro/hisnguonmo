using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Repository;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisAllergyCard
{
    public partial class HisAllergyCardDAO : EntityBase
    {
        private HisAllergyCardCreate CreateWorker
        {
            get
            {
                return (HisAllergyCardCreate)Worker.Get<HisAllergyCardCreate>();
            }
        }
        private HisAllergyCardUpdate UpdateWorker
        {
            get
            {
                return (HisAllergyCardUpdate)Worker.Get<HisAllergyCardUpdate>();
            }
        }
        private HisAllergyCardDelete DeleteWorker
        {
            get
            {
                return (HisAllergyCardDelete)Worker.Get<HisAllergyCardDelete>();
            }
        }
        private HisAllergyCardTruncate TruncateWorker
        {
            get
            {
                return (HisAllergyCardTruncate)Worker.Get<HisAllergyCardTruncate>();
            }
        }
        private HisAllergyCardGet GetWorker
        {
            get
            {
                return (HisAllergyCardGet)Worker.Get<HisAllergyCardGet>();
            }
        }
        private HisAllergyCardCheck CheckWorker
        {
            get
            {
                return (HisAllergyCardCheck)Worker.Get<HisAllergyCardCheck>();
            }
        }

        public bool Create(HIS_ALLERGY_CARD data)
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

        public bool CreateList(List<HIS_ALLERGY_CARD> listData)
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

        public bool Update(HIS_ALLERGY_CARD data)
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

        public bool UpdateList(List<HIS_ALLERGY_CARD> listData)
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

        public bool Delete(HIS_ALLERGY_CARD data)
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

        public bool DeleteList(List<HIS_ALLERGY_CARD> listData)
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

        public bool Truncate(HIS_ALLERGY_CARD data)
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

        public bool TruncateList(List<HIS_ALLERGY_CARD> listData)
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

        public List<HIS_ALLERGY_CARD> Get(HisAllergyCardSO search, CommonParam param)
        {
            List<HIS_ALLERGY_CARD> result = new List<HIS_ALLERGY_CARD>();
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

        public HIS_ALLERGY_CARD GetById(long id, HisAllergyCardSO search)
        {
            HIS_ALLERGY_CARD result = null;
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
