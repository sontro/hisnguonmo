using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHospitalizeReason
{
    public partial class HisHospitalizeReasonManager : BusinessBase
    {
        public HisHospitalizeReasonManager()
            : base()
        {

        }
        
        public HisHospitalizeReasonManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HOSPITALIZE_REASON>> Get(HisHospitalizeReasonFilterQuery filter)
        {
            ApiResultObject<List<HIS_HOSPITALIZE_REASON>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HOSPITALIZE_REASON> resultData = null;
                if (valid)
                {
                    resultData = new HisHospitalizeReasonGet(param).Get(filter);
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
        public ApiResultObject<HIS_HOSPITALIZE_REASON> Create(HIS_HOSPITALIZE_REASON data)
        {
            ApiResultObject<HIS_HOSPITALIZE_REASON> result = new ApiResultObject<HIS_HOSPITALIZE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOSPITALIZE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHospitalizeReasonCreate(param).Create(data);
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
        public ApiResultObject<HIS_HOSPITALIZE_REASON> Update(HIS_HOSPITALIZE_REASON data)
        {
            ApiResultObject<HIS_HOSPITALIZE_REASON> result = new ApiResultObject<HIS_HOSPITALIZE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOSPITALIZE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHospitalizeReasonUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HOSPITALIZE_REASON> ChangeLock(long id)
        {
            ApiResultObject<HIS_HOSPITALIZE_REASON> result = new ApiResultObject<HIS_HOSPITALIZE_REASON>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOSPITALIZE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHospitalizeReasonLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HOSPITALIZE_REASON> Lock(long id)
        {
            ApiResultObject<HIS_HOSPITALIZE_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOSPITALIZE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHospitalizeReasonLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HOSPITALIZE_REASON> Unlock(long id)
        {
            ApiResultObject<HIS_HOSPITALIZE_REASON> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOSPITALIZE_REASON resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHospitalizeReasonLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHospitalizeReasonTruncate(param).Truncate(id);
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
