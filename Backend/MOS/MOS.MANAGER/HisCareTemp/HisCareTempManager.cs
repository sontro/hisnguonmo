using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTemp
{
    public partial class HisCareTempManager : BusinessBase
    {
        public HisCareTempManager()
            : base()
        {

        }
        
        public HisCareTempManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CARE_TEMP>> Get(HisCareTempFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARE_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARE_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisCareTempGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARE_TEMP> Create(HIS_CARE_TEMP data)
        {
            ApiResultObject<HIS_CARE_TEMP> result = new ApiResultObject<HIS_CARE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCareTempCreate(param).Create(data);
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
        public ApiResultObject<HIS_CARE_TEMP> Update(HIS_CARE_TEMP data)
        {
            ApiResultObject<HIS_CARE_TEMP> result = new ApiResultObject<HIS_CARE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCareTempUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CARE_TEMP> ChangeLock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP> result = new ApiResultObject<HIS_CARE_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CARE_TEMP> Lock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CARE_TEMP> Unlock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCareTempTruncate(param).Truncate(id);
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
