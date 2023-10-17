using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUnderEighteen
{
    public partial class HisKskUnderEighteenManager : BusinessBase
    {
        public HisKskUnderEighteenManager()
            : base()
        {

        }
        
        public HisKskUnderEighteenManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_UNDER_EIGHTEEN>> Get(HisKskUnderEighteenFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_UNDER_EIGHTEEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_UNDER_EIGHTEEN> resultData = null;
                if (valid)
                {
                    resultData = new HisKskUnderEighteenGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> Create(HIS_KSK_UNDER_EIGHTEEN data)
        {
            ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_UNDER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNDER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskUnderEighteenCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> Update(HIS_KSK_UNDER_EIGHTEEN data)
        {
            ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_UNDER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNDER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskUnderEighteenUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_UNDER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNDER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUnderEighteenLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> Lock(long id)
        {
            ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNDER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUnderEighteenLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_UNDER_EIGHTEEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNDER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUnderEighteenLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskUnderEighteenTruncate(param).Truncate(id);
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
