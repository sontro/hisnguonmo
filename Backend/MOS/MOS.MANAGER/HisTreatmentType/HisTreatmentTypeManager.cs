using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentType
{
    public partial class HisTreatmentTypeManager : BusinessBase
    {
        public HisTreatmentTypeManager()
            : base()
        {

        }
        
        public HisTreatmentTypeManager(CommonParam param)
            : base(param)
        {

        }

		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_TYPE>> Get(HisTreatmentTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_TYPE> Create(HIS_TREATMENT_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_TYPE> result = new ApiResultObject<HIS_TREATMENT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_TYPE resultData = null;
                if (valid && new HisTreatmentTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_TREATMENT_TYPE> Update(HIS_TREATMENT_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_TYPE> result = new ApiResultObject<HIS_TREATMENT_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_TYPE resultData = null;
                if (valid && new HisTreatmentTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TREATMENT_TYPE> ChangeLock(HIS_TREATMENT_TYPE data)
        {
            ApiResultObject<HIS_TREATMENT_TYPE> result = new ApiResultObject<HIS_TREATMENT_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_TYPE resultData = null;
                if (valid && new HisTreatmentTypeLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_TREATMENT_TYPE data)
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
                    resultData = new HisTreatmentTypeTruncate(param).Truncate(data);
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
