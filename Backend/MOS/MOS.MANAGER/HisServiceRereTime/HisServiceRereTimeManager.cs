using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRereTime
{
    public partial class HisServiceRereTimeManager : BusinessBase
    {
        public HisServiceRereTimeManager()
            : base()
        {

        }
        
        public HisServiceRereTimeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_RERE_TIME>> Get(HisServiceRereTimeFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_RERE_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_RERE_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceRereTimeGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_RERE_TIME> Create(HIS_SERVICE_RERE_TIME data)
        {
            ApiResultObject<HIS_SERVICE_RERE_TIME> result = new ApiResultObject<HIS_SERVICE_RERE_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RERE_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceRereTimeCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERVICE_RERE_TIME> Update(HIS_SERVICE_RERE_TIME data)
        {
            ApiResultObject<HIS_SERVICE_RERE_TIME> result = new ApiResultObject<HIS_SERVICE_RERE_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RERE_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceRereTimeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERVICE_RERE_TIME> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_RERE_TIME> result = new ApiResultObject<HIS_SERVICE_RERE_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RERE_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRereTimeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_RERE_TIME> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_RERE_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RERE_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRereTimeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_RERE_TIME> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_RERE_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RERE_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRereTimeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceRereTimeTruncate(param).Truncate(id);
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
