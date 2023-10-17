using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationResult
{
    public partial class HisVaccinationResultManager : BusinessBase
    {
        public HisVaccinationResultManager()
            : base()
        {

        }
        
        public HisVaccinationResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VACCINATION_RESULT>> Get(HisVaccinationResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_VACCINATION_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VACCINATION_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisVaccinationResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_VACCINATION_RESULT> Create(HIS_VACCINATION_RESULT data)
        {
            ApiResultObject<HIS_VACCINATION_RESULT> result = new ApiResultObject<HIS_VACCINATION_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationResultCreate(param).Create(data);
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
        public ApiResultObject<HIS_VACCINATION_RESULT> Update(HIS_VACCINATION_RESULT data)
        {
            ApiResultObject<HIS_VACCINATION_RESULT> result = new ApiResultObject<HIS_VACCINATION_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VACCINATION_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaccinationResultUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VACCINATION_RESULT> ChangeLock(long id)
        {
            ApiResultObject<HIS_VACCINATION_RESULT> result = new ApiResultObject<HIS_VACCINATION_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationResultLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_RESULT> Lock(long id)
        {
            ApiResultObject<HIS_VACCINATION_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationResultLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VACCINATION_RESULT> Unlock(long id)
        {
            ApiResultObject<HIS_VACCINATION_RESULT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VACCINATION_RESULT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaccinationResultLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaccinationResultTruncate(param).Truncate(id);
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
