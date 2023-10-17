using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMedicalAssessment
{
    public partial class HisMedicalAssessmentManager : BusinessBase
    {
        public HisMedicalAssessmentManager()
            : base()
        {

        }
        
        public HisMedicalAssessmentManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICAL_ASSESSMENT>> Get(HisMedicalAssessmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICAL_ASSESSMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICAL_ASSESSMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicalAssessmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICAL_ASSESSMENT> Create(HIS_MEDICAL_ASSESSMENT data)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_ASSESSMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicalAssessmentCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDICAL_ASSESSMENT> Update(HIS_MEDICAL_ASSESSMENT data)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICAL_ASSESSMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicalAssessmentUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDICAL_ASSESSMENT> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_ASSESSMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalAssessmentLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICAL_ASSESSMENT> Lock(long id)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_ASSESSMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalAssessmentLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICAL_ASSESSMENT> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICAL_ASSESSMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicalAssessmentLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(long TreatmentId)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisMedicalAssessmentTruncate(param).Truncate(TreatmentId);
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
        public ApiResultObject<HisMedicalAssessmentResultSDO> Save(HisMedicalAssessmentSDO data)
        {
            ApiResultObject<HisMedicalAssessmentResultSDO> result = new ApiResultObject<HisMedicalAssessmentResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                HisMedicalAssessmentResultSDO resultData = null;
                if (valid)
                {
                    if (param == null)
                    {
                        param = new CommonParam();
                    }
                    new HisMedicalAssessmentSave(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
