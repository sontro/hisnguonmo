using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCancelReason
{
    public partial class HisCancelReasonManager : BusinessBase
    {
        public HisCancelReasonManager()
            : base()
        {

        }
        
        public HisCancelReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CANCEL_REASON>> Get(HisCancelReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_CANCEL_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CANCEL_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisCancelReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_CANCEL_REASON> Create(HIS_CANCEL_REASON data)
        {
            ApiResultObject<HIS_CANCEL_REASON> result = new ApiResultObject<HIS_CANCEL_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CANCEL_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCancelReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_CANCEL_REASON> Update(HIS_CANCEL_REASON data)
        {
            ApiResultObject<HIS_CANCEL_REASON> result = new ApiResultObject<HIS_CANCEL_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CANCEL_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCancelReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CANCEL_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_CANCEL_REASON> result = new ApiResultObject<HIS_CANCEL_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CANCEL_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCancelReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CANCEL_REASON> Lock(long id)
        {
            ApiResultObject<HIS_CANCEL_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CANCEL_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCancelReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CANCEL_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_CANCEL_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CANCEL_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCancelReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCancelReasonTruncate(param).Truncate(id);
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
