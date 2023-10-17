using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmotionlessResult
{
    public partial class HisEmotionlessResultManager : BusinessBase
    {
        public HisEmotionlessResultManager()
            : base()
        {

        }
        
        public HisEmotionlessResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EMOTIONLESS_RESULT>> Get(HisEmotionlessResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMOTIONLESS_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMOTIONLESS_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisEmotionlessResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMOTIONLESS_RESULT> Create(HIS_EMOTIONLESS_RESULT data)
        {
            ApiResultObject<HIS_EMOTIONLESS_RESULT> result = new ApiResultObject<HIS_EMOTIONLESS_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmotionlessResultCreate(param).Create(data);
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
        public ApiResultObject<HIS_EMOTIONLESS_RESULT> Update(HIS_EMOTIONLESS_RESULT data)
        {
            ApiResultObject<HIS_EMOTIONLESS_RESULT> result = new ApiResultObject<HIS_EMOTIONLESS_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmotionlessResultUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EMOTIONLESS_RESULT> ChangeLock(long id)
        {
            ApiResultObject<HIS_EMOTIONLESS_RESULT> result = new ApiResultObject<HIS_EMOTIONLESS_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMOTIONLESS_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmotionlessResultLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EMOTIONLESS_RESULT> Lock(long id)
        {
            ApiResultObject<HIS_EMOTIONLESS_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMOTIONLESS_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmotionlessResultLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EMOTIONLESS_RESULT> Unlock(long id)
        {
            ApiResultObject<HIS_EMOTIONLESS_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMOTIONLESS_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmotionlessResultLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEmotionlessResultTruncate(param).Truncate(id);
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
