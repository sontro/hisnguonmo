using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientCase
{
    public partial class HisPatientCaseManager : BusinessBase
    {
        public HisPatientCaseManager()
            : base()
        {

        }
        
        public HisPatientCaseManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_CASE>> Get(HisPatientCaseFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_CASE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_CASE> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientCaseGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_CASE> Create(HIS_PATIENT_CASE data)
        {
            ApiResultObject<HIS_PATIENT_CASE> result = new ApiResultObject<HIS_PATIENT_CASE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_CASE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientCaseCreate(param).Create(data);
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
        public ApiResultObject<HIS_PATIENT_CASE> Update(HIS_PATIENT_CASE data)
        {
            ApiResultObject<HIS_PATIENT_CASE> result = new ApiResultObject<HIS_PATIENT_CASE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_CASE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientCaseUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PATIENT_CASE> ChangeLock(long id)
        {
            ApiResultObject<HIS_PATIENT_CASE> result = new ApiResultObject<HIS_PATIENT_CASE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CASE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientCaseLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_CASE> Lock(long id)
        {
            ApiResultObject<HIS_PATIENT_CASE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CASE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientCaseLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_CASE> Unlock(long id)
        {
            ApiResultObject<HIS_PATIENT_CASE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CASE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientCaseLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPatientCaseTruncate(param).Truncate(id);
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
