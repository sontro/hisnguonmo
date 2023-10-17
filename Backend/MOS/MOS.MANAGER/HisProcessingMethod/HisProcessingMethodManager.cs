using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisProcessingMethod
{
    public partial class HisProcessingMethodManager : BusinessBase
    {
        public HisProcessingMethodManager()
            : base()
        {

        }
        
        public HisProcessingMethodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PROCESSING_METHOD>> Get(HisProcessingMethodFilterQuery filter)
        {
            ApiResultObject<List<HIS_PROCESSING_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PROCESSING_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisProcessingMethodGet(param).Get(filter);
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
        public ApiResultObject<HIS_PROCESSING_METHOD> Create(HIS_PROCESSING_METHOD data)
        {
            ApiResultObject<HIS_PROCESSING_METHOD> result = new ApiResultObject<HIS_PROCESSING_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PROCESSING_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisProcessingMethodCreate(param).Create(data);
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
        public ApiResultObject<HIS_PROCESSING_METHOD> Update(HIS_PROCESSING_METHOD data)
        {
            ApiResultObject<HIS_PROCESSING_METHOD> result = new ApiResultObject<HIS_PROCESSING_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PROCESSING_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisProcessingMethodUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PROCESSING_METHOD> ChangeLock(long id)
        {
            ApiResultObject<HIS_PROCESSING_METHOD> result = new ApiResultObject<HIS_PROCESSING_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PROCESSING_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisProcessingMethodLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PROCESSING_METHOD> Lock(long id)
        {
            ApiResultObject<HIS_PROCESSING_METHOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PROCESSING_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisProcessingMethodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PROCESSING_METHOD> Unlock(long id)
        {
            ApiResultObject<HIS_PROCESSING_METHOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PROCESSING_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisProcessingMethodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisProcessingMethodTruncate(param).Truncate(id);
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
