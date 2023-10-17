using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentResult
{
    public partial class HisTreatmentResultManager : BusinessBase
    {
        public HisTreatmentResultManager()
            : base()
        {

        }
        
        public HisTreatmentResultManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_RESULT>> Get(HisTreatmentResultFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_RESULT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_RESULT> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentResultGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_RESULT> Create(HIS_TREATMENT_RESULT data)
        {
            ApiResultObject<HIS_TREATMENT_RESULT> result = new ApiResultObject<HIS_TREATMENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_RESULT resultData = null;
                if (valid && new HisTreatmentResultCreate(param).Create(data))
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
        public ApiResultObject<HIS_TREATMENT_RESULT> Update(HIS_TREATMENT_RESULT data)
        {
            ApiResultObject<HIS_TREATMENT_RESULT> result = new ApiResultObject<HIS_TREATMENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_RESULT resultData = null;
                if (valid && new HisTreatmentResultUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TREATMENT_RESULT> ChangeLock(HIS_TREATMENT_RESULT data)
        {
            ApiResultObject<HIS_TREATMENT_RESULT> result = new ApiResultObject<HIS_TREATMENT_RESULT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_RESULT resultData = null;
                if (valid && new HisTreatmentResultLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_TREATMENT_RESULT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTreatmentResultTruncate(param).Truncate(data);
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
