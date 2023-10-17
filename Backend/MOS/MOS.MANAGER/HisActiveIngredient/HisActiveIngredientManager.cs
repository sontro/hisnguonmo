using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisActiveIngredient
{
    public class HisActiveIngredientManager : BusinessBase
    {
        public HisActiveIngredientManager()
            : base()
        {

        }

        public HisActiveIngredientManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> Get(HisActiveIngredientFilterQuery filter)
        {
            ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ACTIVE_INGREDIENT> resultData = null;
                if (valid)
                {
                    resultData = new HisActiveIngredientGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ACTIVE_INGREDIENT> Create(HIS_ACTIVE_INGREDIENT data)
        {
            ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACTIVE_INGREDIENT resultData = null;
                if (valid && new HisActiveIngredientCreate(param).Create(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ACTIVE_INGREDIENT> Update(HIS_ACTIVE_INGREDIENT data)
        {
            ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ACTIVE_INGREDIENT resultData = null;
                if (valid && new HisActiveIngredientUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_ACTIVE_INGREDIENT> ChangeLock(long id)
        {
            ApiResultObject<HIS_ACTIVE_INGREDIENT> result = new ApiResultObject<HIS_ACTIVE_INGREDIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_ACTIVE_INGREDIENT resultData = null;
                new HisActiveIngredientLock(param).ChangeLock(id, ref resultData);
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_ACTIVE_INGREDIENT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisActiveIngredientTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisActiveIngredientTruncate(param).Truncate(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> TruncateAll()
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisActiveIngredientTruncate(param).TruncateAll();
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> CreateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = new ApiResultObject<List<HIS_ACTIVE_INGREDIENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_ACTIVE_INGREDIENT> resultData = null;
                if (valid && new HisActiveIngredientCreate(param).CreateList(listData))
                {
                    resultData = listData;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> UpdateList(List<HIS_ACTIVE_INGREDIENT> listData)
        {
            ApiResultObject<List<HIS_ACTIVE_INGREDIENT>> result = new ApiResultObject<List<HIS_ACTIVE_INGREDIENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_ACTIVE_INGREDIENT> resultData = null;
                if (valid && new HisActiveIngredientUpdate(param).UpdateList(listData))
                {
                    resultData = listData;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

    }
}
