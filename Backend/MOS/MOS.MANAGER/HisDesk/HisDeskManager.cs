using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    public partial class HisDeskManager : BusinessBase
    {
        public HisDeskManager()
            : base()
        {

        }
        
        public HisDeskManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DESK>> Get(HisDeskFilterQuery filter)
        {
            ApiResultObject<List<HIS_DESK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DESK> resultData = null;
                if (valid)
                {
                    resultData = new HisDeskGet(param).Get(filter);
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
        public ApiResultObject<HIS_DESK> Create(HIS_DESK data)
        {
            ApiResultObject<HIS_DESK> result = new ApiResultObject<HIS_DESK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DESK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDeskCreate(param).Create(data);
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
        public ApiResultObject<HIS_DESK> Update(HIS_DESK data)
        {
            ApiResultObject<HIS_DESK> result = new ApiResultObject<HIS_DESK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DESK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDeskUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DESK> ChangeLock(long id)
        {
            ApiResultObject<HIS_DESK> result = new ApiResultObject<HIS_DESK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DESK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeskLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DESK> Lock(long id)
        {
            ApiResultObject<HIS_DESK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DESK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeskLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DESK> Unlock(long id)
        {
            ApiResultObject<HIS_DESK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DESK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDeskLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDeskTruncate(param).Truncate(id);
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
