using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccAppointment
{
    public partial class HisVaccAppointmentManager : BusinessBase
    {
        public HisVaccAppointmentManager()
            : base()
        {

        }
        
        public HisVaccAppointmentManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACC_APPOINTMENT>> Get(HisVaccAppointmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACC_APPOINTMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACC_APPOINTMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccAppointmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACC_APPOINTMENT> Create(HIS_VACC_APPOINTMENT data)
        {
            ApiResultObject<HIS_VACC_APPOINTMENT> result = new ApiResultObject<HIS_VACC_APPOINTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_APPOINTMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccAppointmentCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACC_APPOINTMENT> Update(HIS_VACC_APPOINTMENT data)
        {
            ApiResultObject<HIS_VACC_APPOINTMENT> result = new ApiResultObject<HIS_VACC_APPOINTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_APPOINTMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccAppointmentUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACC_APPOINTMENT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACC_APPOINTMENT> result = new ApiResultObject<HIS_VACC_APPOINTMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_APPOINTMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccAppointmentLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_APPOINTMENT> Lock(long id)
        {
            ApiResultObject<HIS_VACC_APPOINTMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_APPOINTMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccAppointmentLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_APPOINTMENT> Unlock(long id)
        {
            ApiResultObject<HIS_VACC_APPOINTMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_APPOINTMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccAppointmentLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccAppointmentTruncate(param).Truncate(id);
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
