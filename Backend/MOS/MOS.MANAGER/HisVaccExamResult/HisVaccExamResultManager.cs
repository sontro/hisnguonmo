using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccExamResult
{
    public partial class HisVaccExamResultManager : BusinessBase
    {
        public HisVaccExamResultManager()
            : base()
        {

        }
        
        public HisVaccExamResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACC_EXAM_RESULT>> Get(HisVaccExamResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACC_EXAM_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACC_EXAM_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccExamResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACC_EXAM_RESULT> Create(HIS_VACC_EXAM_RESULT data)
        {
            ApiResultObject<HIS_VACC_EXAM_RESULT> result = new ApiResultObject<HIS_VACC_EXAM_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_EXAM_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccExamResultCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACC_EXAM_RESULT> Update(HIS_VACC_EXAM_RESULT data)
        {
            ApiResultObject<HIS_VACC_EXAM_RESULT> result = new ApiResultObject<HIS_VACC_EXAM_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACC_EXAM_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccExamResultUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACC_EXAM_RESULT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACC_EXAM_RESULT> result = new ApiResultObject<HIS_VACC_EXAM_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_EXAM_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccExamResultLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_EXAM_RESULT> Lock(long id)
        {
            ApiResultObject<HIS_VACC_EXAM_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_EXAM_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccExamResultLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACC_EXAM_RESULT> Unlock(long id)
        {
            ApiResultObject<HIS_VACC_EXAM_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACC_EXAM_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccExamResultLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccExamResultTruncate(param).Truncate(id);
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
