using Inventec.Core;
using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.AcsOtp
{
    public partial class AcsOtpManager : BusinessBase
    {
        public AcsOtpManager()
            : base()
        {

        }
        
        public AcsOtpManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<ACS_OTP>> Get(AcsOtpFilterQuery filter)
        {
            ApiResultObject<List<ACS_OTP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<ACS_OTP> resultData = null;
                if (valid)
                {
                    resultData = new AcsOtpGet(param).Get(filter);
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
        public ApiResultObject<ACS_OTP> Create(ACS_OTP data)
        {
            ApiResultObject<ACS_OTP> result = new ApiResultObject<ACS_OTP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                ACS_OTP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new AcsOtpCreate(param).Create(data);
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
        public ApiResultObject<ACS_OTP> Update(ACS_OTP data)
        {
            ApiResultObject<ACS_OTP> result = new ApiResultObject<ACS_OTP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                ACS_OTP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new AcsOtpUpdate(param).Update(data);
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
        public ApiResultObject<ACS_OTP> ChangeLock(long id)
        {
            ApiResultObject<ACS_OTP> result = new ApiResultObject<ACS_OTP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_OTP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsOtpLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<ACS_OTP> Lock(long id)
        {
            ApiResultObject<ACS_OTP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_OTP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsOtpLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<ACS_OTP> Unlock(long id)
        {
            ApiResultObject<ACS_OTP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ACS_OTP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new AcsOtpLock(param).Unlock(id, ref resultData);
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
                    resultData = new AcsOtpTruncate(param).Truncate(id);
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
