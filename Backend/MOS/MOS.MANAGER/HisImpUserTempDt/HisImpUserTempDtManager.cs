using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    public partial class HisImpUserTempDtManager : BusinessBase
    {
        public HisImpUserTempDtManager()
            : base()
        {

        }
        
        public HisImpUserTempDtManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_USER_TEMP_DT>> Get(HisImpUserTempDtFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_USER_TEMP_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_USER_TEMP_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisImpUserTempDtGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_USER_TEMP_DT> Create(HIS_IMP_USER_TEMP_DT data)
        {
            ApiResultObject<HIS_IMP_USER_TEMP_DT> result = new ApiResultObject<HIS_IMP_USER_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_USER_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpUserTempDtCreate(param).Create(data);
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
        public ApiResultObject<HIS_IMP_USER_TEMP_DT> Update(HIS_IMP_USER_TEMP_DT data)
        {
            ApiResultObject<HIS_IMP_USER_TEMP_DT> result = new ApiResultObject<HIS_IMP_USER_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_USER_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpUserTempDtUpdate(param).Update(data);
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
        public ApiResultObject<HIS_IMP_USER_TEMP_DT> ChangeLock(long id)
        {
            ApiResultObject<HIS_IMP_USER_TEMP_DT> result = new ApiResultObject<HIS_IMP_USER_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_USER_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpUserTempDtLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_USER_TEMP_DT> Lock(long id)
        {
            ApiResultObject<HIS_IMP_USER_TEMP_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_USER_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpUserTempDtLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_USER_TEMP_DT> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_USER_TEMP_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_USER_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpUserTempDtLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisImpUserTempDtTruncate(param).Truncate(id);
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
