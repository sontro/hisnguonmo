using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndType
{
    public partial class HisTreatmentEndTypeManager : BusinessBase
    {
        public HisTreatmentEndTypeManager()
            : base()
        {

        }
        
        public HisTreatmentEndTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_END_TYPE>> Get(HisTreatmentEndTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_END_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_END_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentEndTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE> Create(HIS_TREATMENT_END_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE> result = new ApiResultObject<HIS_TREATMENT_END_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE resultData = null;
                if (valid && new HisTreatmentEndTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE> Update(HIS_TREATMENT_END_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE> result = new ApiResultObject<HIS_TREATMENT_END_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE resultData = null;
                if (valid && new HisTreatmentEndTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE> ChangeLock(HIS_TREATMENT_END_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE> result = new ApiResultObject<HIS_TREATMENT_END_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE resultData = null;
                if (valid && new HisTreatmentEndTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_TREATMENT_END_TYPE data)
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
                    resultData = new HisTreatmentEndTypeTruncate(param).Truncate(data);
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
