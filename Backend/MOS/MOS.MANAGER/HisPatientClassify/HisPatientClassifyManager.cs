using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientClassify
{
    public partial class HisPatientClassifyManager : BusinessBase
    {
        public HisPatientClassifyManager()
            : base()
        {

        }
        
        public HisPatientClassifyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PATIENT_CLASSIFY>> Get(HisPatientClassifyFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT_CLASSIFY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT_CLASSIFY> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientClassifyGet(param).Get(filter);
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
        public ApiResultObject<HIS_PATIENT_CLASSIFY> Create(HIS_PATIENT_CLASSIFY data)
        {
            ApiResultObject<HIS_PATIENT_CLASSIFY> result = new ApiResultObject<HIS_PATIENT_CLASSIFY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_CLASSIFY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientClassifyCreate(param).Create(data);
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
        public ApiResultObject<HIS_PATIENT_CLASSIFY> Update(HIS_PATIENT_CLASSIFY data)
        {
            ApiResultObject<HIS_PATIENT_CLASSIFY> result = new ApiResultObject<HIS_PATIENT_CLASSIFY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT_CLASSIFY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPatientClassifyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PATIENT_CLASSIFY> ChangeLock(long id)
        {
            ApiResultObject<HIS_PATIENT_CLASSIFY> result = new ApiResultObject<HIS_PATIENT_CLASSIFY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CLASSIFY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientClassifyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_CLASSIFY> Lock(long id)
        {
            ApiResultObject<HIS_PATIENT_CLASSIFY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CLASSIFY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientClassifyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PATIENT_CLASSIFY> Unlock(long id)
        {
            ApiResultObject<HIS_PATIENT_CLASSIFY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PATIENT_CLASSIFY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPatientClassifyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPatientClassifyTruncate(param).Truncate(id);
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
