using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCarerCard
{
    public partial class HisCarerCardManager : BusinessBase
    {
        public HisCarerCardManager()
            : base()
        {

        }
        
        public HisCarerCardManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CARER_CARD>> Get(HisCarerCardFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARER_CARD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARER_CARD> resultData = null;
                if (valid)
                {
                    resultData = new HisCarerCardGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_CARER_CARD> Create(HIS_CARER_CARD data)
        {
            ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCarerCardCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_CARER_CARD>> CreateList(List<HIS_CARER_CARD> data)
        {
            ApiResultObject<List<HIS_CARER_CARD>> result = new ApiResultObject<List<HIS_CARER_CARD>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CARER_CARD> resultData = null;
                if (valid && new HisCarerCardCreate(param).CreateList(data))
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
        public ApiResultObject<HIS_CARER_CARD> Update(HIS_CARER_CARD data)
        {
            ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARER_CARD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCarerCardUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_CARER_CARD> ChangeLock(long id)
        {
            ApiResultObject<HIS_CARER_CARD> result = new ApiResultObject<HIS_CARER_CARD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_CARER_CARD> Lock(long id)
        {
            ApiResultObject<HIS_CARER_CARD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_CARER_CARD> Unlock(long id)
        {
            ApiResultObject<HIS_CARER_CARD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARER_CARD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCarerCardLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisCarerCardTruncate(param).Truncate(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Lost(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisCarerCardLost(param).Run(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CancelLost(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisCarerCardCancelLost(param).Run(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
