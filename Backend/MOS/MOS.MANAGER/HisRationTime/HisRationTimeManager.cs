using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    public partial class HisRationTimeManager : BusinessBase
    {
        public HisRationTimeManager()
            : base()
        {

        }
        
        public HisRationTimeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_RATION_TIME>> Get(HisRationTimeFilterQuery filter)
        {
            ApiResultObject<List<HIS_RATION_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RATION_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisRationTimeGet(param).Get(filter);
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
        public ApiResultObject<HIS_RATION_TIME> Create(HIS_RATION_TIME data)
        {
            ApiResultObject<HIS_RATION_TIME> result = new ApiResultObject<HIS_RATION_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationTimeCreate(param).Create(data);
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
        public ApiResultObject<HIS_RATION_TIME> Update(HIS_RATION_TIME data)
        {
            ApiResultObject<HIS_RATION_TIME> result = new ApiResultObject<HIS_RATION_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationTimeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_RATION_TIME> ChangeLock(long id)
        {
            ApiResultObject<HIS_RATION_TIME> result = new ApiResultObject<HIS_RATION_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationTimeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_TIME> Lock(long id)
        {
            ApiResultObject<HIS_RATION_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationTimeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_TIME> Unlock(long id)
        {
            ApiResultObject<HIS_RATION_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationTimeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRationTimeTruncate(param).Truncate(id);
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
