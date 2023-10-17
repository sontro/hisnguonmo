using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateTemp
{
    public partial class HisDebateTempManager : BusinessBase
    {
        public HisDebateTempManager()
            : base()
        {

        }
        
        public HisDebateTempManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBATE_TEMP>> Get(HisDebateTempFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBATE_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBATE_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateTempGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBATE_TEMP> Create(HIS_DEBATE_TEMP data)
        {
            ApiResultObject<HIS_DEBATE_TEMP> result = new ApiResultObject<HIS_DEBATE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateTempCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBATE_TEMP> Update(HIS_DEBATE_TEMP data)
        {
            ApiResultObject<HIS_DEBATE_TEMP> result = new ApiResultObject<HIS_DEBATE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateTempUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBATE_TEMP> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBATE_TEMP> result = new ApiResultObject<HIS_DEBATE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTempLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_TEMP> Lock(long id)
        {
            ApiResultObject<HIS_DEBATE_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTempLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_TEMP> Unlock(long id)
        {
            ApiResultObject<HIS_DEBATE_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTempLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebateTempTruncate(param).Truncate(id);
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
