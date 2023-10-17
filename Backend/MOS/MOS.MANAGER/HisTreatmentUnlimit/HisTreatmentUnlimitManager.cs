using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    public partial class HisTreatmentUnlimitManager : BusinessBase
    {
        public HisTreatmentUnlimitManager()
            : base()
        {

        }
        
        public HisTreatmentUnlimitManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_UNLIMIT>> Get(HisTreatmentUnlimitFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_UNLIMIT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_UNLIMIT> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentUnlimitGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_UNLIMIT> Create(HIS_TREATMENT_UNLIMIT data)
        {
            ApiResultObject<HIS_TREATMENT_UNLIMIT> result = new ApiResultObject<HIS_TREATMENT_UNLIMIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_UNLIMIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentUnlimitCreate(param).Create(data);
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
        public ApiResultObject<HIS_TREATMENT_UNLIMIT> Update(HIS_TREATMENT_UNLIMIT data)
        {
            ApiResultObject<HIS_TREATMENT_UNLIMIT> result = new ApiResultObject<HIS_TREATMENT_UNLIMIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_UNLIMIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentUnlimitUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TREATMENT_UNLIMIT> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_UNLIMIT> result = new ApiResultObject<HIS_TREATMENT_UNLIMIT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_UNLIMIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentUnlimitLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_UNLIMIT> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_UNLIMIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_UNLIMIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentUnlimitLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_UNLIMIT> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_UNLIMIT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_UNLIMIT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentUnlimitLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTreatmentUnlimitTruncate(param).Truncate(id);
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
