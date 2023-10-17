using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineMedicine
{
    public partial class HisMedicineMedicineManager : BusinessBase
    {
        public HisMedicineMedicineManager()
            : base()
        {

        }
        
        public HisMedicineMedicineManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEDICINE_MEDICINE>> Get(HisMedicineMedicineFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEDICINE_MEDICINE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEDICINE_MEDICINE> resultData = null;
                if (valid)
                {
                    resultData = new HisMedicineMedicineGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEDICINE_MEDICINE> Create(HIS_MEDICINE_MEDICINE data)
        {
            ApiResultObject<HIS_MEDICINE_MEDICINE> result = new ApiResultObject<HIS_MEDICINE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineMedicineCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEDICINE_MEDICINE> Update(HIS_MEDICINE_MEDICINE data)
        {
            ApiResultObject<HIS_MEDICINE_MEDICINE> result = new ApiResultObject<HIS_MEDICINE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEDICINE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMedicineMedicineUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEDICINE_MEDICINE> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MEDICINE> result = new ApiResultObject<HIS_MEDICINE_MEDICINE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMedicineLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_MEDICINE> Lock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMedicineLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEDICINE_MEDICINE> Unlock(long id)
        {
            ApiResultObject<HIS_MEDICINE_MEDICINE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEDICINE_MEDICINE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMedicineMedicineLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMedicineMedicineTruncate(param).Truncate(id);
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
