using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceHein
{
    public partial class HisServiceHeinManager : BusinessBase
    {
        public HisServiceHeinManager()
            : base()
        {

        }
        
        public HisServiceHeinManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_HEIN>> Get(HisServiceHeinFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_HEIN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_HEIN> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceHeinGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_HEIN> Create(HIS_SERVICE_HEIN data)
        {
            ApiResultObject<HIS_SERVICE_HEIN> result = new ApiResultObject<HIS_SERVICE_HEIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_HEIN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceHeinCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERVICE_HEIN> Update(HIS_SERVICE_HEIN data)
        {
            ApiResultObject<HIS_SERVICE_HEIN> result = new ApiResultObject<HIS_SERVICE_HEIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_HEIN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceHeinUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERVICE_HEIN> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_HEIN> result = new ApiResultObject<HIS_SERVICE_HEIN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_HEIN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceHeinLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_HEIN> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_HEIN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_HEIN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceHeinLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_HEIN> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_HEIN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_HEIN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceHeinLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceHeinTruncate(param).Truncate(id);
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
