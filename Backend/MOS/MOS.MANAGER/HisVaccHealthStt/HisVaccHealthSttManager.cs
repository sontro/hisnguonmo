using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccHealthStt
{
    public partial class HisVaccHealthSttManager : BusinessBase
    {
        public HisVaccHealthSttManager()
            : base()
        {

        }
        
        public HisVaccHealthSttManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACC_HEALTH_STT>> Get(HisVaccHealthSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACC_HEALTH_STT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACC_HEALTH_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccHealthSttGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACC_HEALTH_STT> Create(HIS_VACC_HEALTH_STT data)
        {
            ApiResultObject<HIS_VACC_HEALTH_STT> result = new ApiResultObject<HIS_VACC_HEALTH_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_HEALTH_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccHealthSttCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACC_HEALTH_STT> Update(HIS_VACC_HEALTH_STT data)
        {
            ApiResultObject<HIS_VACC_HEALTH_STT> result = new ApiResultObject<HIS_VACC_HEALTH_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_HEALTH_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccHealthSttUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACC_HEALTH_STT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACC_HEALTH_STT> result = new ApiResultObject<HIS_VACC_HEALTH_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_HEALTH_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccHealthSttLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_HEALTH_STT> Lock(long id)
        {
            ApiResultObject<HIS_VACC_HEALTH_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_HEALTH_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccHealthSttLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_HEALTH_STT> Unlock(long id)
        {
            ApiResultObject<HIS_VACC_HEALTH_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_HEALTH_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccHealthSttLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccHealthSttTruncate(param).Truncate(id);
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
