using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCacheMonitor
{
    public partial class HisCacheMonitorManager : BusinessBase
    {
        public HisCacheMonitorManager()
            : base()
        {

        }
        
        public HisCacheMonitorManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CACHE_MONITOR>> Get(HisCacheMonitorFilterQuery filter)
        {
            ApiResultObject<List<HIS_CACHE_MONITOR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CACHE_MONITOR> resultData = null;
                if (valid)
                {
                    resultData = new HisCacheMonitorGet(param).Get(filter);
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
        public ApiResultObject<HIS_CACHE_MONITOR> Create(HIS_CACHE_MONITOR data)
        {
            ApiResultObject<HIS_CACHE_MONITOR> result = new ApiResultObject<HIS_CACHE_MONITOR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CACHE_MONITOR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCacheMonitorCreate(param).Create(data);
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
        public ApiResultObject<HIS_CACHE_MONITOR> Update(HIS_CACHE_MONITOR data)
        {
            ApiResultObject<HIS_CACHE_MONITOR> result = new ApiResultObject<HIS_CACHE_MONITOR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CACHE_MONITOR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCacheMonitorUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CACHE_MONITOR> ChangeLock(long id)
        {
            ApiResultObject<HIS_CACHE_MONITOR> result = new ApiResultObject<HIS_CACHE_MONITOR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CACHE_MONITOR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCacheMonitorLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CACHE_MONITOR> Lock(long id)
        {
            ApiResultObject<HIS_CACHE_MONITOR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CACHE_MONITOR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCacheMonitorLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CACHE_MONITOR> Unlock(long id)
        {
            ApiResultObject<HIS_CACHE_MONITOR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CACHE_MONITOR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCacheMonitorLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCacheMonitorTruncate(param).Truncate(id);
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
