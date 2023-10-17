using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSourceMedicine
{
    public partial class HisSourceMedicineManager : BusinessBase
    {
        public HisSourceMedicineManager()
            : base()
        {

        }
        
        public HisSourceMedicineManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SOURCE_MEDICINE>> Get(HisSourceMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_SOURCE_MEDICINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SOURCE_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisSourceMedicineGet(param).Get(filter);
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
        public ApiResultObject<HIS_SOURCE_MEDICINE> Create(HIS_SOURCE_MEDICINE data)
        {
            ApiResultObject<HIS_SOURCE_MEDICINE> result = new ApiResultObject<HIS_SOURCE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SOURCE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSourceMedicineCreate(param).Create(data);
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
        public ApiResultObject<HIS_SOURCE_MEDICINE> Update(HIS_SOURCE_MEDICINE data)
        {
            ApiResultObject<HIS_SOURCE_MEDICINE> result = new ApiResultObject<HIS_SOURCE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SOURCE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSourceMedicineUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SOURCE_MEDICINE> ChangeLock(long id)
        {
            ApiResultObject<HIS_SOURCE_MEDICINE> result = new ApiResultObject<HIS_SOURCE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SOURCE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSourceMedicineLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SOURCE_MEDICINE> Lock(long id)
        {
            ApiResultObject<HIS_SOURCE_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SOURCE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSourceMedicineLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SOURCE_MEDICINE> Unlock(long id)
        {
            ApiResultObject<HIS_SOURCE_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SOURCE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSourceMedicineLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSourceMedicineTruncate(param).Truncate(id);
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
