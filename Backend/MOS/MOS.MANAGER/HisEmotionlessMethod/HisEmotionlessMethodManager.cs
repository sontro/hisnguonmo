using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmotionlessMethod
{
    public partial class HisEmotionlessMethodManager : BusinessBase
    {
        public HisEmotionlessMethodManager()
            : base()
        {

        }
        
        public HisEmotionlessMethodManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_EMOTIONLESS_METHOD>> Get(HisEmotionlessMethodFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMOTIONLESS_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMOTIONLESS_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisEmotionlessMethodGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMOTIONLESS_METHOD> Create(HIS_EMOTIONLESS_METHOD data)
        {
            ApiResultObject<HIS_EMOTIONLESS_METHOD> result = new ApiResultObject<HIS_EMOTIONLESS_METHOD>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_METHOD resultData = null;
                if (valid && new HisEmotionlessMethodCreate(param).Create(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_EMOTIONLESS_METHOD> Update(HIS_EMOTIONLESS_METHOD data)
        {
            ApiResultObject<HIS_EMOTIONLESS_METHOD> result = new ApiResultObject<HIS_EMOTIONLESS_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_METHOD resultData = null;
                if (valid && new HisEmotionlessMethodUpdate(param).Update(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_EMOTIONLESS_METHOD> ChangeLock(HIS_EMOTIONLESS_METHOD data)
        {
            ApiResultObject<HIS_EMOTIONLESS_METHOD> result = new ApiResultObject<HIS_EMOTIONLESS_METHOD>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_METHOD resultData = null;
                if (valid && new HisEmotionlessMethodLock(param).ChangeLock(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<bool> Delete(HIS_EMOTIONLESS_METHOD data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisEmotionlessMethodTruncate(param).Truncate(data);
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
