using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    public partial class HisPatientObservationManager : BusinessBase
    {
        public HisPatientObservationManager()
            : base()
        {

        }
        
        public HisPatientObservationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_OBSERVATION>> Get(HisPatientObservationFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_OBSERVATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_OBSERVATION> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientObservationGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_OBSERVATION> Create(HIS_PATIENT_OBSERVATION data)
        {
            ApiResultObject<HIS_PATIENT_OBSERVATION> result = new ApiResultObject<HIS_PATIENT_OBSERVATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_OBSERVATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientObservationCreate(param).Create(data);
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
        public ApiResultObject<HIS_PATIENT_OBSERVATION> Update(HIS_PATIENT_OBSERVATION data)
        {
            ApiResultObject<HIS_PATIENT_OBSERVATION> result = new ApiResultObject<HIS_PATIENT_OBSERVATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_OBSERVATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientObservationUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PATIENT_OBSERVATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_PATIENT_OBSERVATION> result = new ApiResultObject<HIS_PATIENT_OBSERVATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_OBSERVATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientObservationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_OBSERVATION> Lock(long id)
        {
            ApiResultObject<HIS_PATIENT_OBSERVATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_OBSERVATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientObservationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_OBSERVATION> Unlock(long id)
        {
            ApiResultObject<HIS_PATIENT_OBSERVATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_OBSERVATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientObservationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPatientObservationTruncate(param).Truncate(id);
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
