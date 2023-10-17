using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUnlimitReason
{
    public partial class HisUnlimitReasonManager : BusinessBase
    {
        public HisUnlimitReasonManager()
            : base()
        {

        }
        
        public HisUnlimitReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_UNLIMIT_REASON>> Get(HisUnlimitReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_UNLIMIT_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_UNLIMIT_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisUnlimitReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_UNLIMIT_REASON> Create(HIS_UNLIMIT_REASON data)
        {
            ApiResultObject<HIS_UNLIMIT_REASON> result = new ApiResultObject<HIS_UNLIMIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_UNLIMIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUnlimitReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_UNLIMIT_REASON> Update(HIS_UNLIMIT_REASON data)
        {
            ApiResultObject<HIS_UNLIMIT_REASON> result = new ApiResultObject<HIS_UNLIMIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_UNLIMIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUnlimitReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_UNLIMIT_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_UNLIMIT_REASON> result = new ApiResultObject<HIS_UNLIMIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_UNLIMIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUnlimitReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_UNLIMIT_REASON> Lock(long id)
        {
            ApiResultObject<HIS_UNLIMIT_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_UNLIMIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUnlimitReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_UNLIMIT_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_UNLIMIT_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_UNLIMIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUnlimitReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisUnlimitReasonTruncate(param).Truncate(id);
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
