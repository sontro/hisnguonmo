using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEyeSurgryDesc
{
    public partial class HisEyeSurgryDescManager : BusinessBase
    {
        public HisEyeSurgryDescManager()
            : base()
        {

        }
        
        public HisEyeSurgryDescManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EYE_SURGRY_DESC>> Get(HisEyeSurgryDescFilterQuery filter)
        {
            ApiResultObject<List<HIS_EYE_SURGRY_DESC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EYE_SURGRY_DESC> resultData = null;
                if (valid)
                {
                    resultData = new HisEyeSurgryDescGet(param).Get(filter);
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
        public ApiResultObject<HIS_EYE_SURGRY_DESC> Create(HIS_EYE_SURGRY_DESC data)
        {
            ApiResultObject<HIS_EYE_SURGRY_DESC> result = new ApiResultObject<HIS_EYE_SURGRY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EYE_SURGRY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEyeSurgryDescCreate(param).Create(data);
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
        public ApiResultObject<HIS_EYE_SURGRY_DESC> Update(HIS_EYE_SURGRY_DESC data)
        {
            ApiResultObject<HIS_EYE_SURGRY_DESC> result = new ApiResultObject<HIS_EYE_SURGRY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EYE_SURGRY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEyeSurgryDescUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EYE_SURGRY_DESC> ChangeLock(long id)
        {
            ApiResultObject<HIS_EYE_SURGRY_DESC> result = new ApiResultObject<HIS_EYE_SURGRY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EYE_SURGRY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEyeSurgryDescLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EYE_SURGRY_DESC> Lock(long id)
        {
            ApiResultObject<HIS_EYE_SURGRY_DESC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EYE_SURGRY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEyeSurgryDescLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EYE_SURGRY_DESC> Unlock(long id)
        {
            ApiResultObject<HIS_EYE_SURGRY_DESC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EYE_SURGRY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEyeSurgryDescLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEyeSurgryDescTruncate(param).Truncate(id);
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
