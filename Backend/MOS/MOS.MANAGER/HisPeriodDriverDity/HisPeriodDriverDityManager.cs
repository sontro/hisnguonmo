using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    public partial class HisPeriodDriverDityManager : BusinessBase
    {
        public HisPeriodDriverDityManager()
            : base()
        {

        }
        
        public HisPeriodDriverDityManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PERIOD_DRIVER_DITY>> Get(HisPeriodDriverDityFilterQuery filter)
        {
            ApiResultObject<List<HIS_PERIOD_DRIVER_DITY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PERIOD_DRIVER_DITY> resultData = null;
                if (valid)
                {
                    resultData = new HisPeriodDriverDityGet(param).Get(filter);
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
        public ApiResultObject<HIS_PERIOD_DRIVER_DITY> Create(HIS_PERIOD_DRIVER_DITY data)
        {
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PERIOD_DRIVER_DITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPeriodDriverDityCreate(param).Create(data);
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
        public ApiResultObject<HIS_PERIOD_DRIVER_DITY> Update(HIS_PERIOD_DRIVER_DITY data)
        {
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PERIOD_DRIVER_DITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPeriodDriverDityUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PERIOD_DRIVER_DITY> ChangeLock(long id)
        {
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERIOD_DRIVER_DITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPeriodDriverDityLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PERIOD_DRIVER_DITY> Lock(long id)
        {
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERIOD_DRIVER_DITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPeriodDriverDityLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PERIOD_DRIVER_DITY> Unlock(long id)
        {
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PERIOD_DRIVER_DITY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPeriodDriverDityLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPeriodDriverDityTruncate(param).Truncate(id);
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
