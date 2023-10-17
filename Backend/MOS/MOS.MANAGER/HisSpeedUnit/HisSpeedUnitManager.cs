using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    public partial class HisSpeedUnitManager : BusinessBase
    {
        public HisSpeedUnitManager()
            : base()
        {

        }
        
        public HisSpeedUnitManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SPEED_UNIT>> Get(HisSpeedUnitFilterQuery filter)
        {
            ApiResultObject<List<HIS_SPEED_UNIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SPEED_UNIT> resultData = null;
                if (valid)
                {
                    resultData = new HisSpeedUnitGet(param).Get(filter);
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
        public ApiResultObject<HIS_SPEED_UNIT> Create(HIS_SPEED_UNIT data)
        {
            ApiResultObject<HIS_SPEED_UNIT> result = new ApiResultObject<HIS_SPEED_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SPEED_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSpeedUnitCreate(param).Create(data);
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
        public ApiResultObject<HIS_SPEED_UNIT> Update(HIS_SPEED_UNIT data)
        {
            ApiResultObject<HIS_SPEED_UNIT> result = new ApiResultObject<HIS_SPEED_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SPEED_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSpeedUnitUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SPEED_UNIT> ChangeLock(long id)
        {
            ApiResultObject<HIS_SPEED_UNIT> result = new ApiResultObject<HIS_SPEED_UNIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPEED_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpeedUnitLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SPEED_UNIT> Lock(long id)
        {
            ApiResultObject<HIS_SPEED_UNIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPEED_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpeedUnitLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SPEED_UNIT> Unlock(long id)
        {
            ApiResultObject<HIS_SPEED_UNIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SPEED_UNIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSpeedUnitLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSpeedUnitTruncate(param).Truncate(id);
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
