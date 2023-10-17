using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskPeriodDriver
{
    public partial class HisKskPeriodDriverManager : BusinessBase
    {
        public HisKskPeriodDriverManager()
            : base()
        {

        }
        
        public HisKskPeriodDriverManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_PERIOD_DRIVER>> Get(HisKskPeriodDriverFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_PERIOD_DRIVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_PERIOD_DRIVER> resultData = null;
                if (valid)
                {
                    resultData = new HisKskPeriodDriverGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_PERIOD_DRIVER> Create(HIS_KSK_PERIOD_DRIVER data)
        {
            ApiResultObject<HIS_KSK_PERIOD_DRIVER> result = new ApiResultObject<HIS_KSK_PERIOD_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_PERIOD_DRIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskPeriodDriverCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_PERIOD_DRIVER> Update(HIS_KSK_PERIOD_DRIVER data)
        {
            ApiResultObject<HIS_KSK_PERIOD_DRIVER> result = new ApiResultObject<HIS_KSK_PERIOD_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_PERIOD_DRIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskPeriodDriverUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_PERIOD_DRIVER> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_PERIOD_DRIVER> result = new ApiResultObject<HIS_KSK_PERIOD_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_PERIOD_DRIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskPeriodDriverLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_PERIOD_DRIVER> Lock(long id)
        {
            ApiResultObject<HIS_KSK_PERIOD_DRIVER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_PERIOD_DRIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskPeriodDriverLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_PERIOD_DRIVER> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_PERIOD_DRIVER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_PERIOD_DRIVER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskPeriodDriverLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskPeriodDriverTruncate(param).Truncate(id);
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
