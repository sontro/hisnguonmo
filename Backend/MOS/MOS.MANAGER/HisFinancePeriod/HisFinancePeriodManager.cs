using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    public partial class HisFinancePeriodManager : BusinessBase
    {
        public HisFinancePeriodManager()
            : base()
        {

        }
        
        public HisFinancePeriodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_FINANCE_PERIOD>> Get(HisFinancePeriodFilterQuery filter)
        {
            ApiResultObject<List<HIS_FINANCE_PERIOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_FINANCE_PERIOD> resultData = null;
                if (valid)
                {
                    resultData = new HisFinancePeriodGet(param).Get(filter);
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
        public ApiResultObject<HIS_FINANCE_PERIOD> Create(HIS_FINANCE_PERIOD data)
        {
            ApiResultObject<HIS_FINANCE_PERIOD> result = new ApiResultObject<HIS_FINANCE_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FINANCE_PERIOD resultData = null;
                if (valid && new HisFinancePeriodCreate(param).Create(data, ref resultData))
                {

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

		[Logger]
        public ApiResultObject<HIS_FINANCE_PERIOD> Update(HIS_FINANCE_PERIOD data)
        {
            ApiResultObject<HIS_FINANCE_PERIOD> result = new ApiResultObject<HIS_FINANCE_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FINANCE_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFinancePeriodUpdate(param).Update(data);
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
        public ApiResultObject<HIS_FINANCE_PERIOD> ChangeLock(long id)
        {
            ApiResultObject<HIS_FINANCE_PERIOD> result = new ApiResultObject<HIS_FINANCE_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FINANCE_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFinancePeriodLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_FINANCE_PERIOD> Lock(long id)
        {
            ApiResultObject<HIS_FINANCE_PERIOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FINANCE_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFinancePeriodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_FINANCE_PERIOD> Unlock(long id)
        {
            ApiResultObject<HIS_FINANCE_PERIOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FINANCE_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFinancePeriodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisFinancePeriodTruncate(param).Truncate(id);
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
