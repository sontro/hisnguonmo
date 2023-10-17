using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    public partial class HisDepositReasonManager : BusinessBase
    {
        public HisDepositReasonManager()
            : base()
        {

        }
        
        public HisDepositReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEPOSIT_REASON>> Get(HisDepositReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEPOSIT_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEPOSIT_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisDepositReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEPOSIT_REASON> Create(HIS_DEPOSIT_REASON data)
        {
            ApiResultObject<HIS_DEPOSIT_REASON> result = new ApiResultObject<HIS_DEPOSIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDepositReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEPOSIT_REASON> Update(HIS_DEPOSIT_REASON data)
        {
            ApiResultObject<HIS_DEPOSIT_REASON> result = new ApiResultObject<HIS_DEPOSIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEPOSIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDepositReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEPOSIT_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEPOSIT_REASON> result = new ApiResultObject<HIS_DEPOSIT_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEPOSIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDepositReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEPOSIT_REASON> Lock(long id)
        {
            ApiResultObject<HIS_DEPOSIT_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEPOSIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDepositReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEPOSIT_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_DEPOSIT_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEPOSIT_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDepositReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDepositReasonTruncate(param).Truncate(id);
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
