using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHivTreatment
{
    public partial class HisHivTreatmentManager : BusinessBase
    {
        public HisHivTreatmentManager()
            : base()
        {

        }
        
        public HisHivTreatmentManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HIV_TREATMENT>> Get(HisHivTreatmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_HIV_TREATMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HIV_TREATMENT> resultData = null;
                if (valid)
                {
                    resultData = new HisHivTreatmentGet(param).Get(filter);
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
        public ApiResultObject<HIS_HIV_TREATMENT> Create(HIS_HIV_TREATMENT data)
        {
            ApiResultObject<HIS_HIV_TREATMENT> result = new ApiResultObject<HIS_HIV_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HIV_TREATMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHivTreatmentCreate(param).Create(data);
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
        public ApiResultObject<HIS_HIV_TREATMENT> Update(HIS_HIV_TREATMENT data)
        {
            ApiResultObject<HIS_HIV_TREATMENT> result = new ApiResultObject<HIS_HIV_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HIV_TREATMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHivTreatmentUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HIV_TREATMENT> ChangeLock(long id)
        {
            ApiResultObject<HIS_HIV_TREATMENT> result = new ApiResultObject<HIS_HIV_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HIV_TREATMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHivTreatmentLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HIV_TREATMENT> Lock(long id)
        {
            ApiResultObject<HIS_HIV_TREATMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HIV_TREATMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHivTreatmentLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HIV_TREATMENT> Unlock(long id)
        {
            ApiResultObject<HIS_HIV_TREATMENT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HIV_TREATMENT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHivTreatmentLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHivTreatmentTruncate(param).Truncate(id);
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
