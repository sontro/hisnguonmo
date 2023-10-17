using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItemType
{
    public partial class HisMrCheckItemTypeManager : BusinessBase
    {
        public HisMrCheckItemTypeManager()
            : base()
        {

        }
        
        public HisMrCheckItemTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MR_CHECK_ITEM_TYPE>> Get(HisMrCheckItemTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_MR_CHECK_ITEM_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MR_CHECK_ITEM_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisMrCheckItemTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> Create(HIS_MR_CHECK_ITEM_TYPE data)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> result = new ApiResultObject<HIS_MR_CHECK_ITEM_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckItemTypeCreate(param).Create(data);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> Update(HIS_MR_CHECK_ITEM_TYPE data)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> result = new ApiResultObject<HIS_MR_CHECK_ITEM_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckItemTypeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> result = new ApiResultObject<HIS_MR_CHECK_ITEM_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMrCheckItemTypeTruncate(param).Truncate(id);
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
