using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    public partial class HisDebateReasonManager : BusinessBase
    {
        public HisDebateReasonManager()
            : base()
        {

        }
        
        public HisDebateReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBATE_REASON>> Get(HisDebateReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBATE_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBATE_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBATE_REASON> Create(HIS_DEBATE_REASON data)
        {
            ApiResultObject<HIS_DEBATE_REASON> result = new ApiResultObject<HIS_DEBATE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBATE_REASON> Update(HIS_DEBATE_REASON data)
        {
            ApiResultObject<HIS_DEBATE_REASON> result = new ApiResultObject<HIS_DEBATE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBATE_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBATE_REASON> result = new ApiResultObject<HIS_DEBATE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_REASON> Lock(long id)
        {
            ApiResultObject<HIS_DEBATE_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_DEBATE_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebateReasonTruncate(param).Truncate(id);
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
