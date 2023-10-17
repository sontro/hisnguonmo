using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPtttPriority
{
    public partial class HisPtttPriorityManager : BusinessBase
    {
        public HisPtttPriorityManager()
            : base()
        {

        }
        
        public HisPtttPriorityManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PTTT_PRIORITY>> Get(HisPtttPriorityFilterQuery filter)
        {
            ApiResultObject<List<HIS_PTTT_PRIORITY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PTTT_PRIORITY> resultData = null;
                if (valid)
                {
                    resultData = new HisPtttPriorityGet(param).Get(filter);
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
        public ApiResultObject<HIS_PTTT_PRIORITY> Create(HIS_PTTT_PRIORITY data)
        {
            ApiResultObject<HIS_PTTT_PRIORITY> result = new ApiResultObject<HIS_PTTT_PRIORITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_PRIORITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttPriorityCreate(param).Create(data);
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
        public ApiResultObject<HIS_PTTT_PRIORITY> Update(HIS_PTTT_PRIORITY data)
        {
            ApiResultObject<HIS_PTTT_PRIORITY> result = new ApiResultObject<HIS_PTTT_PRIORITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PTTT_PRIORITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPtttPriorityUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PTTT_PRIORITY> ChangeLock(long id)
        {
            ApiResultObject<HIS_PTTT_PRIORITY> result = new ApiResultObject<HIS_PTTT_PRIORITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_PRIORITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttPriorityLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_PRIORITY> Lock(long id)
        {
            ApiResultObject<HIS_PTTT_PRIORITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_PRIORITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttPriorityLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PTTT_PRIORITY> Unlock(long id)
        {
            ApiResultObject<HIS_PTTT_PRIORITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PTTT_PRIORITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPtttPriorityLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPtttPriorityTruncate(param).Truncate(id);
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
