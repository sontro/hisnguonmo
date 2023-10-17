using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentLogging
{
    public partial class HisTreatmentLoggingManager : BusinessBase
    {
        public HisTreatmentLoggingManager()
            : base()
        {

        }
        
        public HisTreatmentLoggingManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_LOGGING>> Get(HisTreatmentLoggingFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_LOGGING>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_LOGGING> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentLoggingGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_LOGGING> Create(HIS_TREATMENT_LOGGING data)
        {
            ApiResultObject<HIS_TREATMENT_LOGGING> result = new ApiResultObject<HIS_TREATMENT_LOGGING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_LOGGING resultData = null;
                if (valid && new HisTreatmentLoggingCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_TREATMENT_LOGGING> Update(HIS_TREATMENT_LOGGING data)
        {
            ApiResultObject<HIS_TREATMENT_LOGGING> result = new ApiResultObject<HIS_TREATMENT_LOGGING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_LOGGING resultData = null;
                if (valid && new HisTreatmentLoggingUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_TREATMENT_LOGGING> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_LOGGING> result = new ApiResultObject<HIS_TREATMENT_LOGGING>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_LOGGING resultData = null;
                if (valid)
                {
                    new HisTreatmentLoggingLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_LOGGING> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_LOGGING> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_LOGGING resultData = null;
                if (valid)
                {
                    new HisTreatmentLoggingLock(param).Lock(id, ref resultData);
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
		
		[Logger]
        public ApiResultObject<HIS_TREATMENT_LOGGING> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_LOGGING> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_LOGGING resultData = null;
                if (valid)
                {
                    new HisTreatmentLoggingLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTreatmentLoggingTruncate(param).Truncate(id);
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
