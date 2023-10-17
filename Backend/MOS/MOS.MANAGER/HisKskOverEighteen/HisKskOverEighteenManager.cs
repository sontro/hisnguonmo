using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOverEighteen
{
    public partial class HisKskOverEighteenManager : BusinessBase
    {
        public HisKskOverEighteenManager()
            : base()
        {

        }
        
        public HisKskOverEighteenManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_OVER_EIGHTEEN>> Get(HisKskOverEighteenFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_OVER_EIGHTEEN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_OVER_EIGHTEEN> resultData = null;
                if (valid)
                {
                    resultData = new HisKskOverEighteenGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_OVER_EIGHTEEN> Create(HIS_KSK_OVER_EIGHTEEN data)
        {
            ApiResultObject<HIS_KSK_OVER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_OVER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OVER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOverEighteenCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_OVER_EIGHTEEN> Update(HIS_KSK_OVER_EIGHTEEN data)
        {
            ApiResultObject<HIS_KSK_OVER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_OVER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OVER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOverEighteenUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_OVER_EIGHTEEN> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_OVER_EIGHTEEN> result = new ApiResultObject<HIS_KSK_OVER_EIGHTEEN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OVER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOverEighteenLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OVER_EIGHTEEN> Lock(long id)
        {
            ApiResultObject<HIS_KSK_OVER_EIGHTEEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OVER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOverEighteenLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OVER_EIGHTEEN> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_OVER_EIGHTEEN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OVER_EIGHTEEN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOverEighteenLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskOverEighteenTruncate(param).Truncate(id);
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
