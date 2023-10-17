using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttGroupBest
{
    public partial class HisPtttGroupBestManager : BusinessBase
    {
        public HisPtttGroupBestManager()
            : base()
        {

        }
        
        public HisPtttGroupBestManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_GROUP_BEST>> Get(HisPtttGroupBestFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_GROUP_BEST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_GROUP_BEST> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttGroupBestGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_GROUP_BEST> Create(HIS_PTTT_GROUP_BEST data)
        {
            ApiResultObject<HIS_PTTT_GROUP_BEST> result = new ApiResultObject<HIS_PTTT_GROUP_BEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_GROUP_BEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttGroupBestCreate(param).Create(data);
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
        public ApiResultObject<HIS_PTTT_GROUP_BEST> Update(HIS_PTTT_GROUP_BEST data)
        {
            ApiResultObject<HIS_PTTT_GROUP_BEST> result = new ApiResultObject<HIS_PTTT_GROUP_BEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_GROUP_BEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttGroupBestUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PTTT_GROUP_BEST> ChangeLock(long id)
        {
            ApiResultObject<HIS_PTTT_GROUP_BEST> result = new ApiResultObject<HIS_PTTT_GROUP_BEST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_GROUP_BEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttGroupBestLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_GROUP_BEST> Lock(long id)
        {
            ApiResultObject<HIS_PTTT_GROUP_BEST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_GROUP_BEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttGroupBestLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_GROUP_BEST> Unlock(long id)
        {
            ApiResultObject<HIS_PTTT_GROUP_BEST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_GROUP_BEST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttGroupBestLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPtttGroupBestTruncate(param).Truncate(id);
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
