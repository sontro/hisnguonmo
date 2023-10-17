using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSumStt
{
    public partial class HisRationSumSttManager : BusinessBase
    {
        public HisRationSumSttManager()
            : base()
        {

        }
        
        public HisRationSumSttManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_RATION_SUM_STT>> Get(HisRationSumSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_RATION_SUM_STT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RATION_SUM_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisRationSumSttGet(param).Get(filter);
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
        public ApiResultObject<HIS_RATION_SUM_STT> Create(HIS_RATION_SUM_STT data)
        {
            ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationSumSttCreate(param).Create(data);
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
        public ApiResultObject<HIS_RATION_SUM_STT> Update(HIS_RATION_SUM_STT data)
        {
            ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationSumSttUpdate(param).Update(data);
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
        public ApiResultObject<HIS_RATION_SUM_STT> ChangeLock(long id)
        {
            ApiResultObject<HIS_RATION_SUM_STT> result = new ApiResultObject<HIS_RATION_SUM_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumSttLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM_STT> Lock(long id)
        {
            ApiResultObject<HIS_RATION_SUM_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumSttLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM_STT> Unlock(long id)
        {
            ApiResultObject<HIS_RATION_SUM_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumSttLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRationSumSttTruncate(param).Truncate(id);
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
