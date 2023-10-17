using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisOtherPaySource
{
    public partial class HisOtherPaySourceManager : BusinessBase
    {
        public HisOtherPaySourceManager()
            : base()
        {

        }
        
        public HisOtherPaySourceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_OTHER_PAY_SOURCE>> Get(HisOtherPaySourceFilterQuery filter)
        {
            ApiResultObject<List<HIS_OTHER_PAY_SOURCE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_OTHER_PAY_SOURCE> resultData = null;
                if (valid)
                {
                    resultData = new HisOtherPaySourceGet(param).Get(filter);
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
        public ApiResultObject<HIS_OTHER_PAY_SOURCE> Create(HIS_OTHER_PAY_SOURCE data)
        {
            ApiResultObject<HIS_OTHER_PAY_SOURCE> result = new ApiResultObject<HIS_OTHER_PAY_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_OTHER_PAY_SOURCE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisOtherPaySourceCreate(param).Create(data);
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
        public ApiResultObject<HIS_OTHER_PAY_SOURCE> Update(HIS_OTHER_PAY_SOURCE data)
        {
            ApiResultObject<HIS_OTHER_PAY_SOURCE> result = new ApiResultObject<HIS_OTHER_PAY_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_OTHER_PAY_SOURCE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisOtherPaySourceUpdate(param).Update(data);
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
        public ApiResultObject<HIS_OTHER_PAY_SOURCE> ChangeLock(long id)
        {
            ApiResultObject<HIS_OTHER_PAY_SOURCE> result = new ApiResultObject<HIS_OTHER_PAY_SOURCE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OTHER_PAY_SOURCE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisOtherPaySourceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_OTHER_PAY_SOURCE> Lock(long id)
        {
            ApiResultObject<HIS_OTHER_PAY_SOURCE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OTHER_PAY_SOURCE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisOtherPaySourceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_OTHER_PAY_SOURCE> Unlock(long id)
        {
            ApiResultObject<HIS_OTHER_PAY_SOURCE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_OTHER_PAY_SOURCE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisOtherPaySourceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisOtherPaySourceTruncate(param).Truncate(id);
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
