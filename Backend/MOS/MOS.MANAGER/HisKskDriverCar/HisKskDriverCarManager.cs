using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskDriverCar
{
    public partial class HisKskDriverCarManager : BusinessBase
    {
        public HisKskDriverCarManager()
            : base()
        {

        }
        
        public HisKskDriverCarManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_DRIVER_CAR>> Get(HisKskDriverCarFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_DRIVER_CAR>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_DRIVER_CAR> resultData = null;
                if (valid)
                {
                    resultData = new HisKskDriverCarGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_DRIVER_CAR> Create(HIS_KSK_DRIVER_CAR data)
        {
            ApiResultObject<HIS_KSK_DRIVER_CAR> result = new ApiResultObject<HIS_KSK_DRIVER_CAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER_CAR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskDriverCarCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_DRIVER_CAR> Update(HIS_KSK_DRIVER_CAR data)
        {
            ApiResultObject<HIS_KSK_DRIVER_CAR> result = new ApiResultObject<HIS_KSK_DRIVER_CAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER_CAR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskDriverCarUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_DRIVER_CAR> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER_CAR> result = new ApiResultObject<HIS_KSK_DRIVER_CAR>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER_CAR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverCarLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_DRIVER_CAR> Lock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER_CAR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER_CAR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverCarLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_DRIVER_CAR> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER_CAR> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER_CAR resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverCarLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskDriverCarTruncate(param).Truncate(id);
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
