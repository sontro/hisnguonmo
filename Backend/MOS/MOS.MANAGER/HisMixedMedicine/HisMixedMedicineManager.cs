using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMixedMedicine
{
    public partial class HisMixedMedicineManager : BusinessBase
    {
        public HisMixedMedicineManager()
            : base()
        {

        }
        
        public HisMixedMedicineManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MIXED_MEDICINE>> Get(HisMixedMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_MIXED_MEDICINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MIXED_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMixedMedicineGet(param).Get(filter);
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
        public ApiResultObject<HIS_MIXED_MEDICINE> Create(HIS_MIXED_MEDICINE data)
        {
            ApiResultObject<HIS_MIXED_MEDICINE> result = new ApiResultObject<HIS_MIXED_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MIXED_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMixedMedicineCreate(param).Create(data);
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
        public ApiResultObject<HIS_MIXED_MEDICINE> Update(HIS_MIXED_MEDICINE data)
        {
            ApiResultObject<HIS_MIXED_MEDICINE> result = new ApiResultObject<HIS_MIXED_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MIXED_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMixedMedicineUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MIXED_MEDICINE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MIXED_MEDICINE> result = new ApiResultObject<HIS_MIXED_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MIXED_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMixedMedicineLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MIXED_MEDICINE> Lock(long id)
        {
            ApiResultObject<HIS_MIXED_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MIXED_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMixedMedicineLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MIXED_MEDICINE> Unlock(long id)
        {
            ApiResultObject<HIS_MIXED_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MIXED_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMixedMedicineLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMixedMedicineTruncate(param).Truncate(id);
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
