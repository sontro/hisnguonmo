using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAppointmentPeriod
{
    public partial class HisAppointmentPeriodManager : BusinessBase
    {
        public HisAppointmentPeriodManager()
            : base()
        {

        }
        
        public HisAppointmentPeriodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_APPOINTMENT_PERIOD>> Get(HisAppointmentPeriodFilterQuery filter)
        {
            ApiResultObject<List<HIS_APPOINTMENT_PERIOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_APPOINTMENT_PERIOD> resultData = null;
                if (valid)
                {
                    resultData = new HisAppointmentPeriodGet(param).Get(filter);
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
        public ApiResultObject<HIS_APPOINTMENT_PERIOD> Create(HIS_APPOINTMENT_PERIOD data)
        {
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_APPOINTMENT_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAppointmentPeriodCreate(param).Create(data);
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
        public ApiResultObject<HIS_APPOINTMENT_PERIOD> Update(HIS_APPOINTMENT_PERIOD data)
        {
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_APPOINTMENT_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAppointmentPeriodUpdate(param).Update(data);
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
        public ApiResultObject<HIS_APPOINTMENT_PERIOD> ChangeLock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = new ApiResultObject<HIS_APPOINTMENT_PERIOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentPeriodLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_APPOINTMENT_PERIOD> Lock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentPeriodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_APPOINTMENT_PERIOD> Unlock(long id)
        {
            ApiResultObject<HIS_APPOINTMENT_PERIOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_APPOINTMENT_PERIOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAppointmentPeriodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAppointmentPeriodTruncate(param).Truncate(id);
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
