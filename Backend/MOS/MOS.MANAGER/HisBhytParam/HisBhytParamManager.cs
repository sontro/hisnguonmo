using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytParam
{
    public partial class HisBhytParamManager : BusinessBase
    {
        public HisBhytParamManager()
            : base()
        {

        }
        
        public HisBhytParamManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BHYT_PARAM>> Get(HisBhytParamFilterQuery filter)
        {
            ApiResultObject<List<HIS_BHYT_PARAM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BHYT_PARAM> resultData = null;
                if (valid)
                {
                    resultData = new HisBhytParamGet(param).Get(filter);
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
        public ApiResultObject<HIS_BHYT_PARAM> Create(HIS_BHYT_PARAM data)
        {
            ApiResultObject<HIS_BHYT_PARAM> result = new ApiResultObject<HIS_BHYT_PARAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_PARAM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBhytParamCreate(param).Create(data);
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
        public ApiResultObject<HIS_BHYT_PARAM> Update(HIS_BHYT_PARAM data)
        {
            ApiResultObject<HIS_BHYT_PARAM> result = new ApiResultObject<HIS_BHYT_PARAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_PARAM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBhytParamUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BHYT_PARAM> ChangeLock(long id)
        {
            ApiResultObject<HIS_BHYT_PARAM> result = new ApiResultObject<HIS_BHYT_PARAM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_PARAM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBhytParamLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BHYT_PARAM> Lock(long id)
        {
            ApiResultObject<HIS_BHYT_PARAM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_PARAM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBhytParamLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BHYT_PARAM> Unlock(long id)
        {
            ApiResultObject<HIS_BHYT_PARAM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_PARAM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBhytParamLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBhytParamTruncate(param).Truncate(id);
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
