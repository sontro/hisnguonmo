using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    public partial class HisStorageConditionManager : BusinessBase
    {
        public HisStorageConditionManager()
            : base()
        {

        }
        
        public HisStorageConditionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_STORAGE_CONDITION>> Get(HisStorageConditionFilterQuery filter)
        {
            ApiResultObject<List<HIS_STORAGE_CONDITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_STORAGE_CONDITION> resultData = null;
                if (valid)
                {
                    resultData = new HisStorageConditionGet(param).Get(filter);
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
        public ApiResultObject<HIS_STORAGE_CONDITION> Create(HIS_STORAGE_CONDITION data)
        {
            ApiResultObject<HIS_STORAGE_CONDITION> result = new ApiResultObject<HIS_STORAGE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_STORAGE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStorageConditionCreate(param).Create(data);
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
        public ApiResultObject<HIS_STORAGE_CONDITION> Update(HIS_STORAGE_CONDITION data)
        {
            ApiResultObject<HIS_STORAGE_CONDITION> result = new ApiResultObject<HIS_STORAGE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_STORAGE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisStorageConditionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_STORAGE_CONDITION> ChangeLock(long id)
        {
            ApiResultObject<HIS_STORAGE_CONDITION> result = new ApiResultObject<HIS_STORAGE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STORAGE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStorageConditionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_STORAGE_CONDITION> Lock(long id)
        {
            ApiResultObject<HIS_STORAGE_CONDITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STORAGE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStorageConditionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_STORAGE_CONDITION> Unlock(long id)
        {
            ApiResultObject<HIS_STORAGE_CONDITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_STORAGE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisStorageConditionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisStorageConditionTruncate(param).Truncate(id);
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
