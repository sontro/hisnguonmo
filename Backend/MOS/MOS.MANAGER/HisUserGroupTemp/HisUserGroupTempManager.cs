using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserGroupTemp
{
    public partial class HisUserGroupTempManager : BusinessBase
    {
        public HisUserGroupTempManager()
            : base()
        {

        }
        
        public HisUserGroupTempManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_USER_GROUP_TEMP>> Get(HisUserGroupTempFilterQuery filter)
        {
            ApiResultObject<List<HIS_USER_GROUP_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_USER_GROUP_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisUserGroupTempGet(param).Get(filter);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP> Create(HIS_USER_GROUP_TEMP data)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP> result = new ApiResultObject<HIS_USER_GROUP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserGroupTempCreate(param).Create(data);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP> Update(HIS_USER_GROUP_TEMP data)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP> result = new ApiResultObject<HIS_USER_GROUP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_GROUP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserGroupTempUpdate(param).Update(data);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP> ChangeLock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP> result = new ApiResultObject<HIS_USER_GROUP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP> Lock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_GROUP_TEMP> Unlock(long id)
        {
            ApiResultObject<HIS_USER_GROUP_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_GROUP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserGroupTempLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisUserGroupTempTruncate(param).Truncate(id);
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
