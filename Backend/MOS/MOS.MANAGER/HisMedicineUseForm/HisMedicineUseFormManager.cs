using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineUseForm
{
    public partial class HisMedicineUseFormManager : BusinessBase
    {
        public HisMedicineUseFormManager()
            : base()
        {

        }
        
        public HisMedicineUseFormManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_USE_FORM>> Get(HisMedicineUseFormFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_USE_FORM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_USE_FORM> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineUseFormGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_USE_FORM> Create(HIS_MEDICINE_USE_FORM data)
        {
            ApiResultObject<HIS_MEDICINE_USE_FORM> result = new ApiResultObject<HIS_MEDICINE_USE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_USE_FORM resultData = null;
                if (valid && new HisMedicineUseFormCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEDICINE_USE_FORM> Update(HIS_MEDICINE_USE_FORM data)
        {
            ApiResultObject<HIS_MEDICINE_USE_FORM> result = new ApiResultObject<HIS_MEDICINE_USE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_USE_FORM resultData = null;
                if (valid && new HisMedicineUseFormUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEDICINE_USE_FORM> ChangeLock(HIS_MEDICINE_USE_FORM data)
        {
            ApiResultObject<HIS_MEDICINE_USE_FORM> result = new ApiResultObject<HIS_MEDICINE_USE_FORM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_USE_FORM resultData = null;
                if (valid && new HisMedicineUseFormLock(param).ChangeLock(data.ID))
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
        public ApiResultObject<bool> Delete(HIS_MEDICINE_USE_FORM data)
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
                    resultData = new HisMedicineUseFormTruncate(param).Truncate(data);
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
