using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTempDt
{
    public partial class HisUserGroupTempDtManager : BusinessBase
    {
        public HisUserGroupTempDtManager()
            : base()
        {

        }
        
        public HisUserGroupTempDtManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_USER_GROUP_TEMP_DT>> Get(HisUserGroupTempDtFilterQuery filter)
        {
            ApiResultObject<List<HIS_USER_GROUP_TEMP_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_USER_GROUP_TEMP_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisUserGroupTempDtGet(param).Get(filter);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP_DT> Create(HIS_USER_GROUP_TEMP_DT data)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP_DT> result = new ApiResultObject<HIS_USER_GROUP_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserGroupTempDtCreate(param).Create(data);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP_DT> Update(HIS_USER_GROUP_TEMP_DT data)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP_DT> result = new ApiResultObject<HIS_USER_GROUP_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserGroupTempDtUpdate(param).Update(data);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP_DT> ChangeLock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP_DT> result = new ApiResultObject<HIS_USER_GROUP_TEMP_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempDtLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP_DT> Lock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempDtLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP_DT> Unlock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempDtLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisUserGroupTempDtTruncate(param).Truncate(id);
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
