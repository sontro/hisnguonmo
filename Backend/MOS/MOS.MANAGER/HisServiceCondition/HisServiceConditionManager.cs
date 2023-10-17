using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    public partial class HisServiceConditionManager : BusinessBase
    {
        public HisServiceConditionManager()
            : base()
        {

        }
        
        public HisServiceConditionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_CONDITION>> Get(HisServiceConditionFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_CONDITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_CONDITION> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceConditionGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_CONDITION> Create(HIS_SERVICE_CONDITION data)
        {
            ApiResultObject<HIS_SERVICE_CONDITION> result = new ApiResultObject<HIS_SERVICE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceConditionCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_SERVICE_CONDITION>> CreateList(List<HIS_SERVICE_CONDITION> data)
        {
            ApiResultObject<List<HIS_SERVICE_CONDITION>> result = new ApiResultObject<List<HIS_SERVICE_CONDITION>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_CONDITION> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceConditionCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_SERVICE_CONDITION> Update(HIS_SERVICE_CONDITION data)
        {
            ApiResultObject<HIS_SERVICE_CONDITION> result = new ApiResultObject<HIS_SERVICE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceConditionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERVICE_CONDITION> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_CONDITION> result = new ApiResultObject<HIS_SERVICE_CONDITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceConditionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_CONDITION> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_CONDITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceConditionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_CONDITION> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_CONDITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_CONDITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceConditionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceConditionTruncate(param).Truncate(id);
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
