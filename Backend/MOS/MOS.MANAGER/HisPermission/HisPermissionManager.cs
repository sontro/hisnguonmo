using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPermission
{
    public partial class HisPermissionManager : BusinessBase
    {
        public HisPermissionManager()
            : base()
        {

        }
        
        public HisPermissionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PERMISSION>> Get(HisPermissionFilterQuery filter)
        {
            ApiResultObject<List<HIS_PERMISSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PERMISSION> resultData = null;
                if (valid)
                {
                    resultData = new HisPermissionGet(param).Get(filter);
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
        public ApiResultObject<HIS_PERMISSION> Create(HIS_PERMISSION data)
        {
            ApiResultObject<HIS_PERMISSION> result = new ApiResultObject<HIS_PERMISSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PERMISSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPermissionCreate(param).Create(data);
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
        public ApiResultObject<HIS_PERMISSION> Update(HIS_PERMISSION data)
        {
            ApiResultObject<HIS_PERMISSION> result = new ApiResultObject<HIS_PERMISSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PERMISSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPermissionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PERMISSION> ChangeLock(long id)
        {
            ApiResultObject<HIS_PERMISSION> result = new ApiResultObject<HIS_PERMISSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERMISSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPermissionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PERMISSION> Lock(long id)
        {
            ApiResultObject<HIS_PERMISSION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERMISSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPermissionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PERMISSION> Unlock(long id)
        {
            ApiResultObject<HIS_PERMISSION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERMISSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPermissionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPermissionTruncate(param).Truncate(id);
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
