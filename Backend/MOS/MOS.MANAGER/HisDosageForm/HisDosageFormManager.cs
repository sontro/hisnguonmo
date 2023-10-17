using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDosageForm
{
    public partial class HisDosageFormManager : BusinessBase
    {
        public HisDosageFormManager()
            : base()
        {

        }
        
        public HisDosageFormManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DOSAGE_FORM>> Get(HisDosageFormFilterQuery filter)
        {
            ApiResultObject<List<HIS_DOSAGE_FORM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DOSAGE_FORM> resultData = null;
                if (valid)
                {
                    resultData = new HisDosageFormGet(param).Get(filter);
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
        public ApiResultObject<HIS_DOSAGE_FORM> Create(HIS_DOSAGE_FORM data)
        {
            ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DOSAGE_FORM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDosageFormCreate(param).Create(data);
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
        public ApiResultObject<HIS_DOSAGE_FORM> Update(HIS_DOSAGE_FORM data)
        {
            ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DOSAGE_FORM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDosageFormUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DOSAGE_FORM> ChangeLock(long id)
        {
            ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOSAGE_FORM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDosageFormLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DOSAGE_FORM> Lock(long id)
        {
            ApiResultObject<HIS_DOSAGE_FORM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOSAGE_FORM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDosageFormLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DOSAGE_FORM> Unlock(long id)
        {
            ApiResultObject<HIS_DOSAGE_FORM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOSAGE_FORM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDosageFormLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDosageFormTruncate(param).Truncate(id);
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
