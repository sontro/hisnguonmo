using Inventec.Common.Repository;
using Inventec.Core;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisActiveIngredient
{
    public partial class HisActiveIngredientDAO : EntityBase
    {
        private HisActiveIngredientCreate CreateWorker
        {
            get
            {
                return (HisActiveIngredientCreate)Worker.Get<HisActiveIngredientCreate>();
            }
        }
        private HisActiveIngredientUpdate UpdateWorker
        {
            get
            {
                return (HisActiveIngredientUpdate)Worker.Get<HisActiveIngredientUpdate>();
            }
        }
        private HisActiveIngredientDelete DeleteWorker
        {
            get
            {
                return (HisActiveIngredientDelete)Worker.Get<HisActiveIngredientDelete>();
            }
        }
        private HisActiveIngredientTruncate TruncateWorker
        {
            get
            {
                return (HisActiveIngredientTruncate)Worker.Get<HisActiveIngredientTruncate>();
            }
        }
        private HisActiveIngredientGet GetWorker
        {
            get
            {
                return (HisActiveIngredientGet)Worker.Get<HisActiveIngredientGet>();
            }
        }
        private HisActiveIngredientCheck CheckWorker
        {
            get
            {
                return (HisActiveIngredientCheck)Worker.Get<HisActiveIngredientCheck>();
            }
        }

        public bool Create(HIS_ACTIVE_INGREDIENT data)
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

        public bool CreateList(List<HIS_ACTIVE_INGREDIENT> listData)
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

        public bool Update(HIS_ACTIVE_INGREDIENT data)
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

        public bool UpdateList(List<HIS_ACTIVE_INGREDIENT> listData)
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

        public bool Delete(HIS_ACTIVE_INGREDIENT data)
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

        public bool DeleteList(List<HIS_ACTIVE_INGREDIENT> listData)
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

        public bool Truncate(HIS_ACTIVE_INGREDIENT data)
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

        public bool TruncateList(List<HIS_ACTIVE_INGREDIENT> listData)
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

        public List<HIS_ACTIVE_INGREDIENT> Get(HisActiveIngredientSO search, CommonParam param)
        {
            List<HIS_ACTIVE_INGREDIENT> result = new List<HIS_ACTIVE_INGREDIENT>();
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

        public HIS_ACTIVE_INGREDIENT GetById(long id, HisActiveIngredientSO search)
        {
            HIS_ACTIVE_INGREDIENT result = null;
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
