using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    public partial class HisEmrCoverConfigManager : BusinessBase
    {
        public HisEmrCoverConfigManager()
            : base()
        {

        }
        
        public HisEmrCoverConfigManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EMR_COVER_CONFIG>> Get(HisEmrCoverConfigFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMR_COVER_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMR_COVER_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisEmrCoverConfigGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMR_COVER_CONFIG> Create(HIS_EMR_COVER_CONFIG data)
        {
            ApiResultObject<HIS_EMR_COVER_CONFIG> result = new ApiResultObject<HIS_EMR_COVER_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_COVER_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmrCoverConfigCreate(param).Create(data);
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
        public ApiResultObject<HIS_EMR_COVER_CONFIG> Update(HIS_EMR_COVER_CONFIG data)
        {
            ApiResultObject<HIS_EMR_COVER_CONFIG> result = new ApiResultObject<HIS_EMR_COVER_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_COVER_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmrCoverConfigUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EMR_COVER_CONFIG> ChangeLock(long id)
        {
            ApiResultObject<HIS_EMR_COVER_CONFIG> result = new ApiResultObject<HIS_EMR_COVER_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_COVER_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrCoverConfigLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EMR_COVER_CONFIG> Lock(long id)
        {
            ApiResultObject<HIS_EMR_COVER_CONFIG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_COVER_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrCoverConfigLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EMR_COVER_CONFIG> Unlock(long id)
        {
            ApiResultObject<HIS_EMR_COVER_CONFIG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMR_COVER_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmrCoverConfigLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEmrCoverConfigTruncate(param).Truncate(id);
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
