using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVitaminA
{
    public partial class HisVitaminAManager : BusinessBase
    {
        public HisVitaminAManager()
            : base()
        {

        }
        
        public HisVitaminAManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VITAMIN_A>> Get(HisVitaminAFilterQuery filter)
        {
            ApiResultObject<List<HIS_VITAMIN_A>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VITAMIN_A> resultData = null;
                if (valid)
                {
                    resultData = new HisVitaminAGet(param).Get(filter);
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
        public ApiResultObject<HIS_VITAMIN_A> Update(HIS_VITAMIN_A data)
        {
            ApiResultObject<HIS_VITAMIN_A> result = new ApiResultObject<HIS_VITAMIN_A>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VITAMIN_A resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVitaminAUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VITAMIN_A> ChangeLock(long id)
        {
            ApiResultObject<HIS_VITAMIN_A> result = new ApiResultObject<HIS_VITAMIN_A>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VITAMIN_A resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVitaminALock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VITAMIN_A> Lock(long id)
        {
            ApiResultObject<HIS_VITAMIN_A> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VITAMIN_A resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVitaminALock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VITAMIN_A> Unlock(long id)
        {
            ApiResultObject<HIS_VITAMIN_A> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VITAMIN_A resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVitaminALock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVitaminATruncate(param).Truncate(id);
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
