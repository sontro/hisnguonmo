using COS.SDO;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.CosCard;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCard
{
    public partial class HisCardManager : BusinessBase
    {
        public HisCardManager()
            : base()
        {

        }

        public HisCardManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_CARD>> Get(HisCardFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARD>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisCardGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_CARD>> GetView(HisCardViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_CARD>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisCardGet(param).GetView(filter);
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
        public ApiResultObject<HisCardSDO> GetCardSdoByCode(string code)
        {
            ApiResultObject<HisCardSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisCardSDO resultData = null;
                if (valid)
                {
                    resultData = new HisCardGet(param).GetCardSdoByCode(code);
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
        public ApiResultObject<V_HIS_CARD> GetCardByCode(string code)
        {
            ApiResultObject<V_HIS_CARD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                V_HIS_CARD resultData = null;
                if (valid)
                {
                    resultData = new HisCardGet(param).GetCardByCode(code);
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
        public ApiResultObject<HIS_CARD> Create(HIS_CARD data)
        {
            ApiResultObject<HIS_CARD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARD resultData = null;
                if (valid && new HisCardCreate(param).Create(data))
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
        public ApiResultObject<HIS_CARD> CreateOrUpdate(HIS_CARD data)
        {
            ApiResultObject<HIS_CARD> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARD resultData = null;
                if (valid && new HisCardCreate(param).CreateOrUpdate(data))
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
        public ApiResultObject<HIS_CARD> Update(HIS_CARD data)
        {
            ApiResultObject<HIS_CARD> result = new ApiResultObject<HIS_CARD>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARD resultData = null;
                if (valid && new HisCardUpdate(param).Update(data))
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
        public ApiResultObject<bool> ChangeLock(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisCardLock(param).ChangeLock(id);
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
        public ApiResultObject<bool> Delete(HIS_CARD data)
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
                    resultData = new HisCardTruncate(param).Truncate(data);
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
        public ApiResultObject<List<HisCardSDO>> GetCardSdoByPhone(string code)
        {
            ApiResultObject<List<HisCardSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisCardSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisCardGet(param).GetCardSdoByPhone(code);
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
    }
}
