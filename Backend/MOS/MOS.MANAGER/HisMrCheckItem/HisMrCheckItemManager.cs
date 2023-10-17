using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItem
{
    public partial class HisMrCheckItemManager : BusinessBase
    {
        public HisMrCheckItemManager()
            : base()
        {

        }
        
        public HisMrCheckItemManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MR_CHECK_ITEM>> Get(HisMrCheckItemFilterQuery filter)
        {
            ApiResultObject<List<HIS_MR_CHECK_ITEM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MR_CHECK_ITEM> resultData = null;
                if (valid)
                {
                    resultData = new HisMrCheckItemGet(param).Get(filter);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM> Create(HIS_MR_CHECK_ITEM data)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM> result = new ApiResultObject<HIS_MR_CHECK_ITEM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckItemCreate(param).Create(data);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM> Update(HIS_MR_CHECK_ITEM data)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM> result = new ApiResultObject<HIS_MR_CHECK_ITEM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_ITEM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckItemUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM> ChangeLock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM> result = new ApiResultObject<HIS_MR_CHECK_ITEM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM> Lock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_ITEM> Unlock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_ITEM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_ITEM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckItemLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMrCheckItemTruncate(param).Truncate(id);
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
