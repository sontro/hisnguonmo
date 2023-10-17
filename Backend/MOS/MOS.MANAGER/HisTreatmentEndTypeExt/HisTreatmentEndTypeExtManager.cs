using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    public partial class HisTreatmentEndTypeExtManager : BusinessBase
    {
        public HisTreatmentEndTypeExtManager()
            : base()
        {

        }
        
        public HisTreatmentEndTypeExtManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_END_TYPE_EXT>> Get(HisTreatmentEndTypeExtFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_END_TYPE_EXT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_END_TYPE_EXT> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentEndTypeExtGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> Create(HIS_TREATMENT_END_TYPE_EXT data)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> result = new ApiResultObject<HIS_TREATMENT_END_TYPE_EXT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE_EXT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentEndTypeExtCreate(param).Create(data);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> Update(HIS_TREATMENT_END_TYPE_EXT data)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> result = new ApiResultObject<HIS_TREATMENT_END_TYPE_EXT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_END_TYPE_EXT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentEndTypeExtUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> result = new ApiResultObject<HIS_TREATMENT_END_TYPE_EXT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_END_TYPE_EXT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentEndTypeExtLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_END_TYPE_EXT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentEndTypeExtLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_END_TYPE_EXT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_END_TYPE_EXT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentEndTypeExtLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTreatmentEndTypeExtTruncate(param).Truncate(id);
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
