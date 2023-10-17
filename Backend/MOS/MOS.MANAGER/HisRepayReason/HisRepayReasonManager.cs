using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRepayReason
{
    public partial class HisRepayReasonManager : BusinessBase
    {
        public HisRepayReasonManager()
            : base()
        {

        }
        
        public HisRepayReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REPAY_REASON>> Get(HisRepayReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_REPAY_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REPAY_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisRepayReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_REPAY_REASON> Create(HIS_REPAY_REASON data)
        {
            ApiResultObject<HIS_REPAY_REASON> result = new ApiResultObject<HIS_REPAY_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REPAY_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRepayReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_REPAY_REASON> Update(HIS_REPAY_REASON data)
        {
            ApiResultObject<HIS_REPAY_REASON> result = new ApiResultObject<HIS_REPAY_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REPAY_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRepayReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_REPAY_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_REPAY_REASON> result = new ApiResultObject<HIS_REPAY_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REPAY_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRepayReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REPAY_REASON> Lock(long id)
        {
            ApiResultObject<HIS_REPAY_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REPAY_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRepayReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REPAY_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_REPAY_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REPAY_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRepayReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRepayReasonTruncate(param).Truncate(id);
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
