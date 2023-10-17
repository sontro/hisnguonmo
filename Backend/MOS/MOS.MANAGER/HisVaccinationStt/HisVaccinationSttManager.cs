using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationStt
{
    public partial class HisVaccinationSttManager : BusinessBase
    {
        public HisVaccinationSttManager()
            : base()
        {

        }
        
        public HisVaccinationSttManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACCINATION_STT>> Get(HisVaccinationSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_STT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationSttGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACCINATION_STT> Create(HIS_VACCINATION_STT data)
        {
            ApiResultObject<HIS_VACCINATION_STT> result = new ApiResultObject<HIS_VACCINATION_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationSttCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACCINATION_STT> Update(HIS_VACCINATION_STT data)
        {
            ApiResultObject<HIS_VACCINATION_STT> result = new ApiResultObject<HIS_VACCINATION_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationSttUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACCINATION_STT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_STT> result = new ApiResultObject<HIS_VACCINATION_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationSttLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_STT> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationSttLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_STT> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationSttLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccinationSttTruncate(param).Truncate(id);
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
