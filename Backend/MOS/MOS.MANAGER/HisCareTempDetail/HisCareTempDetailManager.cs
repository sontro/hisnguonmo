using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareTempDetail
{
    public partial class HisCareTempDetailManager : BusinessBase
    {
        public HisCareTempDetailManager()
            : base()
        {

        }
        
        public HisCareTempDetailManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CARE_TEMP_DETAIL>> Get(HisCareTempDetailFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARE_TEMP_DETAIL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARE_TEMP_DETAIL> resultData = null;
                if (valid)
                {
                    resultData = new HisCareTempDetailGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARE_TEMP_DETAIL> Create(HIS_CARE_TEMP_DETAIL data)
        {
            ApiResultObject<HIS_CARE_TEMP_DETAIL> result = new ApiResultObject<HIS_CARE_TEMP_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TEMP_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCareTempDetailCreate(param).Create(data);
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
        public ApiResultObject<HIS_CARE_TEMP_DETAIL> Update(HIS_CARE_TEMP_DETAIL data)
        {
            ApiResultObject<HIS_CARE_TEMP_DETAIL> result = new ApiResultObject<HIS_CARE_TEMP_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARE_TEMP_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCareTempDetailUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CARE_TEMP_DETAIL> ChangeLock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP_DETAIL> result = new ApiResultObject<HIS_CARE_TEMP_DETAIL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempDetailLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CARE_TEMP_DETAIL> Lock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempDetailLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CARE_TEMP_DETAIL> Unlock(long id)
        {
            ApiResultObject<HIS_CARE_TEMP_DETAIL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARE_TEMP_DETAIL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCareTempDetailLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCareTempDetailTruncate(param).Truncate(id);
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
